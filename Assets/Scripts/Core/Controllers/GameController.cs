using CuteGothicCatcher.Entities;
using CuteGothicCatcher.UI;
using UnityEngine;

namespace CuteGothicCatcher.Core.Controllers
{
    public class GameController : Controller
    {
        [Header("Game Objects")]
        [SerializeField] private GamePanel m_GamePanel;
        [SerializeField] private GameContent m_GameContent;
        [SerializeField] private TimerGameOverPanel m_TimerGameOverPanel;
        
        [Header("Controllers")]
        [SerializeField] private ScoreController m_ScoreController;
        [SerializeField] private EntitiesController m_EntitiesController;
        [SerializeField] private InterfaceController m_InterfaceController;
        [SerializeField] private GameTimerController m_GameTimerController;
        [SerializeField] private PlacedItemsContoller m_PlacedItemsContoller;

        private static bool m_IsGameActive;
        private int m_CollectedHearts;

        public static bool IsGameActive => m_IsGameActive;

        public override void Init()
        {
            m_IsGameActive = false;
            m_GameContent.Init();

            m_GameTimerController.OnTimerEnd = GameTimerEnd;

            EventManager.OnCollectEntity.AddListener(CollectEntity);
        }

        private void CollectEntity(EntityType type, float points)
        {
            if (type == EntityType.Heart)
                m_CollectedHearts++;
        }

        private void GameTimerEnd()
        {
            m_IsGameActive = false;

            m_TimerGameOverPanel.SetScore(m_ScoreController.CurrentScore);
            m_TimerGameOverPanel.SetHearts(m_CollectedHearts);
            m_InterfaceController.OpenPanel(typeof(TimerGameOverPanel));
        }

        public void StartGame()
        {
            m_IsGameActive = true;
            m_CollectedHearts = 0;

            m_ScoreController.ClearScore();

            m_GameTimerController.SetTime(120);
            m_GameTimerController.StartTimer();

            m_EntitiesController.SpawnEntities(EntityType.Heart, 10);
            m_EntitiesController.SpawnEntities(EntityType.Scull, 10);
            //m_EntitiesController.SpawnEntities(EntityType.Web, 8);
            //m_EntitiesController.SpawnEntities(EntityType.Tombstone, 5);
        }
        public void StopGame()
        {
            m_IsGameActive = false;

            m_EntitiesController.RemoveEntities();
            m_PlacedItemsContoller.DisactiveSlots();
            m_GameTimerController.DisableTimer();
            m_ScoreController.SaveScore();
        }

        private void SpawnEntity(EntityType type, int amount = 1)
        {
            m_EntitiesController.SpawnEntities(type, amount);
        }

        private void Update()
        {
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
