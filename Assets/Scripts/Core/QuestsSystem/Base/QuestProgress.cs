using Newtonsoft.Json;
using UnityEngine;

namespace CuteGothicCatcher.Core
{
    [System.Serializable]
    public struct QuestProgress
    {
        [JsonProperty("current")]
        [SerializeField] private float m_Current;
        [JsonProperty("required")]
        [SerializeField] private float m_Required;

        [JsonIgnore]
        public float Current => (float)System.Math.Round(m_Current, 2);
        [JsonIgnore]
        public float Required => (float)System.Math.Round(m_Required, 2);
        [JsonIgnore]
        public bool IsComplete => m_Current >= m_Required;

        public QuestProgress(float current, float required)
        {
            m_Current = current;
            m_Required = required;
        }

        public void SetProgress(float value, bool isTotal)
        {
            if (isTotal)
            {
                if (value > m_Current)
                    m_Current = value;
            }
            else
            {
                m_Current += value;
            }
        }
    }
}
