using CuteGothicCatcher.UI;
using System.Collections.Generic;
using UnityEngine;

namespace CuteGothicCatcher.Core.Controllers
{
    public class GameController : Controller
    {
        [Header("Game Objects")]
        [SerializeField] private GamePanel m_GamePanel;
        [SerializeField] private GameContent m_GameContent;
        
        [Header("Controllers")]
        [SerializeField] private List<GameModeController> m_GameModeControllers;

        private GameModeController m_CurrentModeController;

        private static bool m_IsGameActive;
        public static bool IsGameActive => m_IsGameActive;

        public override void Init()
        {
            m_IsGameActive = false;

            m_GameContent.Init();
            foreach (var controller in m_GameModeControllers)
            {
                controller.OnGameEnd = GameEnd;
                controller.Init();
            }
        }

        #region Game Methods
        public void StartGame(GameMode mode)
        {
            m_IsGameActive = true;

            m_CurrentModeController = m_GameModeControllers.Find(c => c.GameMode == mode);
            m_CurrentModeController.StartGame();
        }
        public void StopGame()
        {
            m_IsGameActive = false;

            m_CurrentModeController.StopGame();
        }
        public void RestartGame()
        {
            m_IsGameActive = true;

            m_CurrentModeController.RestartGame();
        }
        private void GameEnd()
        {
            m_IsGameActive = false;
        }
        #endregion
    }
}
