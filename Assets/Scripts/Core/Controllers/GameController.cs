using CuteGothicCatcher.Entities;
using CuteGothicCatcher.UI;
using UnityEngine;

namespace CuteGothicCatcher.Core.Controllers
{
    public class GameController : Controller
    {
        [SerializeField] private GamePanel m_GamePanel;
        [SerializeField] private GameContent m_GameContent;

        [SerializeField] private EntitiesController m_EntitiesController;

        private bool m_IsGameActive;

        public bool IsGameActive => m_IsGameActive;

        public override void Init()
        {
            m_IsGameActive = false;

            m_GameContent.Init();
        }

        public void StartGame()
        {
            m_IsGameActive = true;

            m_EntitiesController.SpawnEntities(EntityType.Heart, 10);
            m_EntitiesController.SpawnEntities(EntityType.Scull, 10);
            //m_EntitiesController.SpawnEntities(EntityType.Web, 8);
            //m_EntitiesController.SpawnEntities(EntityType.Tombstone, 5);
        }
        public void StopGame()
        {
            m_IsGameActive = false;

            m_EntitiesController.RemoveEntities();
        }

        private void SpawnEntity(EntityType type, int amount = 1)
        {
            m_EntitiesController.SpawnEntities(type, amount);
        }

        private void Update()
        {
            /*if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SpawnEntity(EntityType.Heart);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SpawnEntity(EntityType.Scull);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SpawnEntity(EntityType.Tombstone);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                SpawnEntity(EntityType.Web);
            }*/
        }
    }
}
