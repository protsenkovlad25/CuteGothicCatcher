using CuteGothicCatcher.Core.Controllers;
using Newtonsoft.Json;
using UnityEngine;

namespace CuteGothicCatcher.Core
{
    [System.Serializable]
    public struct QuestReward
    {
        [JsonProperty("type")]
        [SerializeField] private QuestRewardType m_Type;
        [JsonProperty("value")]
        [SerializeField] private int m_Value;

        [JsonIgnore]
        public QuestRewardType Type => m_Type;
        [JsonIgnore]
        public int Value => m_Value;

        public QuestReward(QuestRewardType type, int value)
        {
            m_Type = type;
            m_Value = value;
        }

        public void Give()
        {
            switch (m_Type)
            {
                case QuestRewardType.Hearts:
                    for (int i = 0; i < m_Value; i++)
                        PlayerController.PlayerData.CollectEntity(Entities.EntityType.Heart, 0);
                    break;

                default:
                    Debug.LogError("Reward type - None");
                    break;
            }
        }
    }
}
