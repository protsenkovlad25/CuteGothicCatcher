using CuteGothicCatcher.UI;
using UnityEngine;
using UnityEngine.Events;

namespace CuteGothicCatcher.Core.Controllers
{
    public class GameTimerController : Controller
    {
        public static UnityEvent OnTimerEnd = new();

        [SerializeField] private GameTimer m_GameTimer;

        private float m_Time;
        private Timer m_Timer;

        public float PassedTime => m_Timer.PassedTime;

        public override void Init()
        {
            m_GameTimer.Init();

            EventManager.OnSetExtraTime.AddListener(SetExtraTime);
        }

        public void SetTimerTime(float time)
        {
            m_Time = time;
        }
        public void SetExtraTime(float time)
        {
            m_Timer.SetTime(time);
        }

        #region Timer Methods
        public void StartTimer()
        {
            m_Timer = new Timer(m_Time);
            m_Timer.OnTimesUp.AddListener(EndTimer);

            m_GameTimer.SetAreaAmount(1);
            m_GameTimer.PlayRoteteAnim();
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
            DisableTimer();

            OnTimerEnd?.Invoke();
        }
        public void DisableTimer()
        {
            m_Timer = null;

            m_GameTimer.SetAreaAmount(0);
            m_GameTimer.StopRotateAnim();
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
