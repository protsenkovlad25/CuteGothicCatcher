using CuteGothicCatcher.Core.Controllers;
using CuteGothicCatcher.Core.Interfaces;
using CuteGothicCatcher.UI;
using UnityEngine;
using UnityEngine.Events;

namespace CuteGothicCatcher.Core
{
    public class GameManager : MonoBehaviour, IIniting
    {
        [HideInInspector] public static UnityEvent<float> OnTimeScaleChanged = new UnityEvent<float>();

        [SerializeField] private GameController m_GameController;
        [SerializeField] private ScoreController m_ScoreController;
        [SerializeField] private QuestsController m_QuestsController;
        [SerializeField] private EntitiesController m_EntitiesController;
        [SerializeField] private InterfaceController m_InterfaceController;
        [SerializeField] private GameTimerController m_GameTimerController;
        [SerializeField] private PlacedItemsContoller m_PlacedItemsContoller;

        #region Init
        public void Init()
        {
            PoolResources.LoadAll();

            PlayerController.Init();

            m_InterfaceController.Init();

            m_GameController.Init();
            m_ScoreController.Init();
            m_QuestsController.Init();
            m_EntitiesController.Init();
            m_GameTimerController.Init();
            m_PlacedItemsContoller.Init();

            m_InterfaceController.BlackScreen.Disactivate(InitGame);
        }
        private void InitGame()
        {
            m_InterfaceController.OpenPanel(typeof(GameNamePanel));
            m_InterfaceController.OpenPanel(typeof(MenuPanel));
            m_InterfaceController.OpenPanel(typeof(ScorePanel));
            m_InterfaceController.OpenPanel(typeof(ItemsPanel));
        }
        #endregion

        #region Game Methods
        public void StartGame(int mode)
        {
            StartGame((GameMode)mode);
        }
        public void StartGame(GameMode mode)
        {
            m_InterfaceController.ClosePanel(typeof(GameNamePanel));
            m_InterfaceController.ClosePanel(typeof(GameModesPanel));
            m_InterfaceController.ClosePanel(typeof(ScorePanel));
            m_InterfaceController.OpenPanel(typeof(GamePanel));
            m_InterfaceController.MoveBackground(() => { m_GameController.StartGame(mode); });
        }
        public void StopGame()
        {
            ReturnToMenu();
            m_InterfaceController.MoveBackground();

            m_GameController.StopGame();

            ChangeTimeScale(1);
        }
        public void RestartGame()
        {
            m_GameController.RestartGame();

            ChangeTimeScale(1);
        }
        public void PauseGame()
        {
            ChangeTimeScale(0);

            m_InterfaceController.OpenPanel(typeof(PausePanel));
        }
        public void ContinueGame()
        {
            m_InterfaceController.ClosePanel(typeof(PausePanel), () => { ChangeTimeScale(1); });
        }
        public void ReturnToMenu()
        {
            m_InterfaceController.CloseAllOpenedPanels();
            m_InterfaceController.OpenPanel(typeof(GameNamePanel));
            m_InterfaceController.OpenPanel(typeof(MenuPanel));
            m_InterfaceController.OpenPanel(typeof(ScorePanel));
            m_InterfaceController.OpenPanel(typeof(ItemsPanel));
        }
        public void QuitGame()
        {
            Application.Quit();
        }
        #endregion

        public void ChangeTimeScale(float timeScale)
        {
            Debug.Log($"Time scale changed to - {timeScale}");

            Time.timeScale = timeScale;

            OnTimeScaleChanged?.Invoke(timeScale);
        }

        public void ClearItems()
        {
            PlayerController.PlayerData.ClearItems();
        }
    }
}
