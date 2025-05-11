using CuteGothicCatcher.UI;
using UnityEngine;
using UnityEngine.Events;

namespace CuteGothicCatcher.Core.Controllers
{
    public class ScoreController : Controller
    {
        public static UnityEvent<int> OnUpdatedScore = new ();

        [SerializeField] private ScorePanel m_ScorePanel;
        [SerializeField] private ScorePanel m_BestScorePanel;

        private int m_CurrentScore;

        public int CurrentScore => m_CurrentScore;

        public override void Init()
        {
            m_ScorePanel.Init();
            m_BestScorePanel.SetScoreValue(PlayerController.PlayerData.MaxScore);

            ClearScore();

            EventManager.OnSetScorePoints.AddListener(SetScorePoints);
        }

        public void ClearScore()
        {
            m_CurrentScore = 0;
            UpdateScore();
        }
        public void SetScorePoints(int points)
        {
            m_CurrentScore += points;
            UpdateScore();
        }
        public void SaveScore()
        {
            PlayerController.PlayerData.SetScore(m_CurrentScore);
            m_BestScorePanel.SetScoreValue(PlayerController.PlayerData.MaxScore);
        }
        public void UpdateScore()
        {
            m_ScorePanel.SetScoreValue(m_CurrentScore);

            OnUpdatedScore?.Invoke(m_CurrentScore);
        }
    }
}
