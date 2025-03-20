using CuteGothicCatcher.UI;
using UnityEngine;

namespace CuteGothicCatcher.Core.Controllers
{
    public class ScoreController : Controller
    {
        [SerializeField] private ScorePanel m_ScorePanel;

        private int m_CurrentScore;

        public int CurrentScore => m_CurrentScore;

        public override void Init()
        {
            m_ScorePanel.Init();

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
        }

        public void UpdateScore()
        {
            m_ScorePanel.SetScoreValue(m_CurrentScore);
        }
    }
}
