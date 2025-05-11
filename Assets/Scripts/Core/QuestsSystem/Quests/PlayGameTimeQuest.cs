namespace CuteGothicCatcher.Core
{
    [System.Serializable]
    public class PlayGameTimeQuest : Quest
    {
        private Timer m_Timer;

        protected override void Subscribe()
        {
            StartTimer();
        }

        protected override void Unsubscribe()
        {
            StopTimer();
        }

        private void StartTimer()
        {
            m_Timer = new Timer(m_Data.Progress.Required);
            m_Timer.OnTimesUp.AddListener(EndTimer);
        }
        private void StopTimer()
        {
            m_Timer.Pause();
        }
        private void EndTimer()
        {
            Complete();
        }

        private void Update()
        {
            m_Timer?.Update();
            UpdateProgress((int)m_Timer.PassedTime, true);
        }
    }
}
