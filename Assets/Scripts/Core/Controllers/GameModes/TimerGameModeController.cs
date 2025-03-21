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

        private int m_CollectedHearts;
        private float m_NextSpawnTime;

        private TimerGameModeConfig m_Config;
        private List<SpawnEntityWeight> m_SpawnEntities;

        public override GameMode GameMode => GameMode.Timer;

        public override void Init()
        {
            m_Config = PoolResources.TimerGMConfig;
            
            m_SpawnEntities = new List<SpawnEntityWeight>();
            m_SpawnEntities.AddRange(m_Config.SpawnEntities);

            m_GameTimerController.OnTimerEnd = GameTimerEnd;

            EventManager.OnCollectEntity.AddListener(CollectEntity);
        }

        #region Game Methods
        public override void StartGame()
        {
            enabled = true;

            m_CollectedHearts = 0;
            m_NextSpawnTime = 0;

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
            m_InterfaceController.OpenPanel(typeof(TimerGameOverPanel));
        }

        private void CollectEntity(EntityType type, float points)
        {
            if (type == EntityType.Heart)
                m_CollectedHearts++;
        }

        private void SpawnEntity(EntityType type, int amount = 1)
        {
            m_EntitiesController.SpawnEntities(type, amount);
        }

        private void CheckNextSpawn()
        {
            if (Time.time >= m_NextSpawnTime)
            {
                SpawnEntity(GetRandomSpawnEntity());
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
                    en.ChangeWeight(en.Weight * 0.8f); // Уменьшаем вес за частый спавн
                }
                else
                {
                    en.ChangeWeight(en.Weight * 1.1f); // Увеличиваем шанс на редкие сущности
                }
            }
        }

        private void Update()
        {
            if (GameController.IsGameActive)
                CheckNextSpawn();

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
