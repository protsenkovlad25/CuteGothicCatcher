using CuteGothicCatcher.UI;
using UnityEngine;

namespace CuteGothicCatcher.Core.Controllers
{
    public class GameTimerController : Controller
    {
        public System.Action OnTimerEnd;

        [SerializeField] private GameTimer m_GameTimer;

        private float m_Time;
        private Timer m_Timer;

        public override void Init()
        {
            m_GameTimer.Init();
        }

        public void SetTime(float time)
        {
            m_Time = time;
        }

        #region Timer Methods
        public void StartTimer()
        {
            m_Timer = new Timer(m_Time);
            m_Timer.OnTimesUp.AddListener(EndTimer);

            m_GameTimer.SetAreaAmount(1);
        }
        public void PlayTimer()
        {
            m_Timer.Play();
        }
        public void PauseTimer()
        {
            m_Timer.Pause();
        }
        public void EndTimer()
        {
            m_Timer = null;
            m_GameTimer.SetAreaAmount(0);

            OnTimerEnd?.Invoke();
        }
        public void UpdateTimerAndArea()
        {
            if (m_Timer != null)
            {
                m_GameTimer.SetAreaAmount(m_Timer.CurrentTime / m_Time);
                m_Timer.Update();
            }
        }
        #endregion

        private void Update()
        {
            UpdateTimerAndArea();
        }
    }
}
