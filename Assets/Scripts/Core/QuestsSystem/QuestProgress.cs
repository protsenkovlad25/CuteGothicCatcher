using UnityEngine;

namespace CuteGothicCatcher.Core
{
    [System.Serializable]
    public class QuestProgress
    {
        [SerializeField] private int m_Current;
        [SerializeField] private int m_Required;

        public int Current => m_Current;
        public int Required => m_Required;
        public bool IsComplete => m_Current >= m_Required;

        public QuestProgress(int current, int required)
        {
            m_Current = current;
            m_Required = required;
        }

        public void SetProgress(int value)
        {
            m_Current += value;
        }
    }
}
