using TMPro;
using UnityEngine;

namespace CuteGothicCatcher.Core
{
    public class FPSCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_FPSText;
        [SerializeField] private bool m_IsGraphicsControl;

        private Timer m_FPSTimer;

        private bool m_IsActive;
        private int m_CountOfFrames;

        private void Start()
        {
            StartCalculate();
        }

        private void StartCalculate()
        {
            m_FPSTimer = new Timer(.5f);
            m_FPSTimer.OnTimesUp.AddListener(RestartCalculator);

            m_CountOfFrames = 0;
            m_IsActive = true;
        }
        private void StopCalculate()
        {
            m_FPSTimer.Pause();
            m_IsActive = false;
        }
        public void RestartCalculator()
        {
            m_FPSTimer?.Reset();
            m_FPSText.text = (m_CountOfFrames * 2).ToString();

            m_CountOfFrames = 0;
            m_IsActive = true;
        }

        private void Update()
        {
            if (m_IsActive)
            {
                m_CountOfFrames++;

                m_FPSTimer?.Update(false);
            }
        }
    }
}
