using CuteGothicCatcher.Core.Controllers;

namespace CuteGothicCatcher.Core
{
    [System.Serializable]
    public class GetScoreQuest : Quest
    {
        protected override void Subscribe()
        {
            ScoreController.OnUpdatedScore.AddListener(SetScore);
        }

        protected override void Unsubscribe()
        {
            ScoreController.OnUpdatedScore.RemoveListener(SetScore);
        }

        public void SetScore(int score)
        {
            UpdateProgress(score, true);
        }
    }
}
