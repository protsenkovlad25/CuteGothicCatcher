using CuteGothicCatcher.Entities;
using CuteGothicCatcher.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace CuteGothicCatcher.Core.Controllers
{
    public class TimerGameModeController : GameModeController
    {
        public static UnityEvent OnEndTimerGame = new();

        [SerializeField] private TimerGameOverPanel m_GameOverPanel;

        [Header("Controllers")]
        [SerializeField] private ScoreController m_ScoreController;
        [SerializeField] private QuestsController m_QuestsController;
        [SerializeField] private EntitiesController m_EntitiesController;
        [SerializeField] private InterfaceController m_InterfaceController;
        [SerializeField] private GameTimerController m_GameTimerController;
        [SerializeField] private PlacedItemsContoller m_PlacedItemsContoller;

        private int m_SpendedHearts;
        private int m_CollectedHearts;
        private float m_GameProgress;
        private float m_GameStartTime;
        private float m_NextSpawnTime;
        private float m_MultipleSpawnChance;

        private TimerGameModeConfig m_Config;
        private List<SpawnEntityWeight> m_SpawnEntities;
        private Dictionary<EntityType, int> m_CollectedEntities;
        private Dictionary<EntityType, int> m_DestroyedEntities;

        public override GameMode GameMode => GameMode.Timer;

        public override void Init()
        {
            m_Config = PoolResources.TimerGMConfig;

            InitSpawnEntities();
            InitEntitiesLists();

            GameTimerController.OnTimerEnd.AddListener(GameTimerEnd);

            EventManager.OnCollectEntity.AddListener(CollectEntity);
            EventManager.OnEntityDied.AddListener(DieEntity);
            PlayerController.PlayerData.OnSpendedHearts += SpendHearts;
        }

        private void InitSpawnEntities()
        {
            m_SpawnEntities = new List<SpawnEntityWeight>();
            m_SpawnEntities.AddRange(m_Config.SpawnEntities);
        }
        private void InitEntitiesLists()
        {
            m_CollectedEntities = new Dictionary<EntityType, int>();
            m_DestroyedEntities = new Dictionary<EntityType, int>();

            foreach (EntityType type in System.Enum.GetValues(typeof(EntityType)))
            {
                if (type != EntityType.None)
                {
                    m_CollectedEntities.Add(type, 0);
                    m_DestroyedEntities.Add(type, 0);
                }
            }
        }

        #region Game Methods
        public override void StartGame()
        {
            enabled = true;

            m_GameStartTime = Time.time;
            m_GameProgress = 0;
            m_NextSpawnTime = 0;
            m_SpendedHearts = 0;
            m_CollectedHearts = 0;
            m_MultipleSpawnChance = 0;

            InitSpawnEntities();
            InitEntitiesLists();

            m_ScoreController.ClearScore();
            m_QuestsController.CreateLocalQuests(m_Config.QuestsCount);

            m_GameTimerController.SetTimerTime(m_Config.TimerTime);
            m_GameTimerController.StartTimer();
        }
        public override void StopGame()
        {
            enabled = false;

            m_EntitiesController.RemoveEntities();
            m_PlacedItemsContoller.DisactiveSlots();
            m_GameTimerController.DisableTimer();
            m_ScoreController.SaveScore();
            m_QuestsController.DestroyLocalQuests();
        }
        public override void RestartGame()
        {
            m_QuestsController.DestroyLocalQuests();

            m_EntitiesController.RemoveEntities();
            m_InterfaceController.ClosePanel(typeof(TimerGameOverPanel));

            StartGame();
        }
        #endregion

        private void GameTimerEnd()
        {
            OnGameEnd?.Invoke();
            OnEndTimerGame?.Invoke();

            m_ScoreController.SaveScore();
            m_QuestsController.GiveQuestRewards();

            m_GameOverPanel.SetScore(m_ScoreController.CurrentScore);
            m_GameOverPanel.SetHearts(m_CollectedHearts);
            m_GameOverPanel.SetSpendedHearts(m_SpendedHearts);
            m_GameOverPanel.SetCollectedEntities(m_CollectedEntities);
            m_GameOverPanel.SetDestroyedEntities(m_DestroyedEntities);
            m_InterfaceController.OpenPanel(typeof(TimerGameOverPanel));
        }

        private void CollectEntity(EntityType type, float points)
        {
            if (GameController.IsGameActive)
                m_CollectedEntities[type]++;
        }
        private void DieEntity(BaseEntity entity)
        {
            if (GameController.IsGameActive)
                m_DestroyedEntities[entity.Data.EntityType]++;
        }

        private void SpendHearts(int amount)
        {
            if (GameController.IsGameActive)
                m_SpendedHearts += amount;
        }

        private void SpawnEntity(EntityType type, EntitySubType subType = EntitySubType.Ordinary)
        {
            m_EntitiesController.SpawnEntity(type, subType);
        }
        private void SpawnEntities()
        {
            int count = 1;

            if (Random.value < m_MultipleSpawnChance)
                count = Random.Range(m_Config.MinMultipleSpawnCount, m_Config.MaxMultipleSpawnCount + 1);
            
            (EntityType, EntitySubType) en;
            for (int i = 0; i < count; i++)
            {
                en = GetRandomSpawnEntity();
                
                if (en.Item1 != EntityType.None)
                    SpawnEntity(en.Item1, en.Item2);
            }
        }

        private void CheckNextSpawn()
        {
            if (Time.time >= m_NextSpawnTime)
            {
                SpawnEntities();
                m_NextSpawnTime = Time.time + -Mathf.Log(1 - Random.value) / m_Config.Intensity;
            }
        }

        public (EntityType, EntitySubType) GetRandomSpawnEntity()
        {
            List<SpawnEntityWeight> entities = new List<SpawnEntityWeight>();
            entities.AddRange(m_SpawnEntities);

            int count;
            for (int i = entities.Count - 1; i >= 0; i--)
            {
                count = m_EntitiesController.ActiveEntities.Count(e => e.Data.EntityType == entities[i].EntityType &&
                                                                       e.Data.EntitySubType == entities[i].EntitySubType);
                
                if (count > entities[i].MaxNum)
                    entities.RemoveAt(i);
            }

            if (entities.Count == 0)
                return (EntityType.None, EntitySubType.Ordinary);

            float totalWeight = entities.Sum(e => e.Weight);
            float randValue = Random.Range(0f, totalWeight);
            float cumulativeWeight = 0;

            foreach (var en in entities)
            {
                cumulativeWeight += en.Weight;
                if (randValue < cumulativeWeight)
                {
                    AdjustWeightsAfterSpawn(en.EntityType, en.EntitySubType);
                    return (en.EntityType, en.EntitySubType);
                }
            }

            throw new System.Exception("Couldn't get a random entity");
        }

        private void AdjustWeightsAfterSpawn(EntityType type, EntitySubType subType)
        {
            foreach (var en in m_SpawnEntities)
            {
                if (en.EntityType == type && en.EntitySubType == subType)
                {
                    en.ChangeWeight(en.Weight * m_Config.MultiplierSpawnedEntity);
                }
                else
                {
                    en.ChangeWeight(en.Weight * m_Config.MultiplierOtherEntity);
                }
            }
        }

        private void NormalizeGameProgress()
        {
            m_GameProgress = Mathf.Clamp01(m_GameTimerController.PassedTime / m_Config.TimerTime);

            m_MultipleSpawnChance = Mathf.Lerp(m_Config.MinMultipleSpawnChance, m_Config.MaxMultipleSpawnChance, m_GameProgress);
        }

        private void Update()
        {
            if (GameController.IsGameActive)
            {
                NormalizeGameProgress();
                CheckNextSpawn();
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                SpawnEntity(EntityType.Heart);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                SpawnEntity(EntityType.Scull);
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                SpawnEntity(EntityType.Tombstone);
            }
        }
    }
}
