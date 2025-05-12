using CuteGothicCatcher.Core.Controllers;

namespace CuteGothicCatcher.Core
{
    [System.Serializable]
    public class PlayTimerModeQuest : Quest
    {
        protected override void Subscribe()
        {
            GameTimerController.OnTimerEnd.AddListener(EndTimerMode);
        }

        protected override void Unsubscribe()
        {
            GameTimerController.OnTimerEnd.RemoveListener(EndTimerMode);
        }

        private void EndTimerMode()
        {
            UpdateProgress(1);
        }
    }
}
