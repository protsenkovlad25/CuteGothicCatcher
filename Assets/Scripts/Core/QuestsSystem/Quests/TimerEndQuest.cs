using CuteGothicCatcher.Core.Controllers;

namespace CuteGothicCatcher.Core
{
    [System.Serializable]
    public class TimerEndQuest : Quest
    {
        protected override void Subscribe()
        {
            TimerGameModeController.OnEndTimerGame.AddListener(TimerEnd);
        }

        protected override void Unsubscribe()
        {
            TimerGameModeController.OnEndTimerGame.RemoveListener(TimerEnd);
        }

        private void TimerEnd()
        {
            UpdateProgress(1);
        }
    }
}
