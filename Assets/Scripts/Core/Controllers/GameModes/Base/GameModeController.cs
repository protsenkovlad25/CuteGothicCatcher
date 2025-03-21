using CuteGothicCatcher.UI;
using UnityEngine;

namespace CuteGothicCatcher.Core.Controllers
{
    public abstract class GameModeController : Controller
    {
        public System.Action OnGameEnd;

        [Header("Game Objects")]
        [SerializeField] private GamePanel m_GamePanel;
        [SerializeField] private GameContent m_GameContant;

        public abstract GameMode GameMode { get; }

        public abstract void StartGame();
        public abstract void StopGame();
        public abstract void RestartGame();
    }
}
