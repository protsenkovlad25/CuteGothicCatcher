using CuteGothicCatcher.Entities;
using CuteGothicCatcher.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CuteGothicCatcher.Core.Controllers
{
    public class TimerGameModeController : GameModeController
    {
        [SerializeField] private TimerGameOverPanel m_GameOverPanel;

        [Header("Controllers")]
        [SerializeField] private ScoreController m_ScoreController;
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

            m_GameTimerController.OnTimerEnd = GameTimerEnd;

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

            m_GameTimerController.SetTime(m_Config.TimerTime);
            m_GameTimerController.StartTimer();

            //m_EntitiesController.SpawnEntities(EntityType.Heart, 10);
            //m_EntitiesController.SpawnEntities(EntityType.Scull, 10);
            //m_EntitiesController.SpawnEntities(EntityType.Web, 8);
            //m_EntitiesController.SpawnEntities(EntityType.Tombstone, 5);
        }
        public override void StopGame()
        {
            enabled = false;

            m_EntitiesController.RemoveEntities();
            m_PlacedItemsContoller.DisactiveSlots();
            m_GameTimerController.DisableTimer();
            m_ScoreController.SaveScore();
        }
        public override void RestartGame()
        {
            m_EntitiesController.RemoveEntities();
            m_InterfaceController.ClosePanel(typeof(TimerGameOverPanel));

            StartGame();
        }
        #endregion

        private void GameTimerEnd()
        {
            OnGameEnd?.Invoke();

            m_ScoreController.SaveScore();

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

        private void SpawnEntity(EntityType type)
        {
            m_EntitiesController.SpawnEntity(type);
        }
        private void SpawnEntities()
        {
            int count = 1;

            if (Random.value < m_MultipleSpawnChance)
                count = Random.Range(m_Config.MinMultipleSpawnCount, m_Config.MaxMultipleSpawnCount + 1);

            for (int i = 0; i < count; i++)
            {
                SpawnEntity(GetRandomSpawnEntity());
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

        public EntityType GetRandomSpawnEntity()
        {
            float totalWeight = m_SpawnEntities.Sum(e => e.Weight);
            float randValue = Random.Range(0f, totalWeight);
            float cumulativeWeight = 0;

            foreach (var en in m_SpawnEntities)
            {
                cumulativeWeight += en.Weight;
                if (randValue < cumulativeWeight)
                {
                    AdjustWeightsAfterSpawn(en.EntityType);
                    return en.EntityType;
                }
            }

            throw new System.Exception("Couldn't get a random entity");
        }

        private void AdjustWeightsAfterSpawn(EntityType type)
        {
            foreach (var en in m_SpawnEntities)
            {
                if (en.EntityType == type)
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
