using CuteGothicCatcher.Core.Controllers;
using Newtonsoft.Json;
using UnityEngine;

namespace CuteGothicCatcher.Core
{
    [System.Serializable]
    public class QuestReward
    {
        [JsonProperty("type")]
        [SerializeField] private QuestRewardType m_Type;
        [JsonProperty("value")]
        [SerializeField] private int m_Value;

        public QuestRewardType Type => m_Type;
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
                    PlayerController.PlayerData.CollectEntity(Entities.EntityType.Heart, m_Value);
                    break;

                default:
                    Debug.LogError("Reward type - None");
                    break;
            }
        }
    }
}
