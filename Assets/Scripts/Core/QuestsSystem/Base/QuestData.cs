using Newtonsoft.Json;
using UnityEngine;

namespace CuteGothicCatcher.Core
{
    [System.Serializable]
    public struct QuestData
    {
        #region SerializeFields
        [JsonProperty("id")]
        [SerializeField] private string m_Id;
        [JsonProperty("title")]
        [SerializeField] private string m_Title;
        [JsonProperty("description")]
        [SerializeField] private string m_Description;
        [JsonProperty("type")]
        [SerializeField] private QuestType m_Type;
        [JsonProperty("questProgress")]
        [SerializeField] private QuestProgress m_Progress;
        [JsonProperty("questReward")]
        [SerializeField] private QuestReward m_Reward;
        [JsonProperty("isReceived")]
        [SerializeField] private bool m_IsReceived;
        #endregion

        #region Properties
        [JsonIgnore]
        public string Id => m_Id;
        [JsonIgnore]
        public string Title => m_Title;
        [JsonIgnore]
        public string Description => m_Description.Replace("#", m_Progress.Required.ToString());
        [JsonIgnore]
        public QuestType Type => m_Type;
        [JsonIgnore]
        public QuestProgress Progress => m_Progress;
        [JsonIgnore]
        public QuestReward Reward => m_Reward;
        [JsonIgnore]
        public bool IsReceived => m_IsReceived;
        [JsonIgnore]
        public bool IsComplete => m_Progress.IsComplete;
        #endregion

        #region Constructors
        public QuestData(QuestData data)
        {
            m_Id = data.Id;
            m_Title = data.Title;
            m_Description = data.Description;
            m_Type = data.Type;
            m_Progress = new QuestProgress(data.Progress.Current, data.Progress.Required);
            m_Reward = new QuestReward(data.Reward.Type, data.Reward.Value);
            m_IsReceived = data.IsReceived;
        }
        
        public QuestData(
            string id,
            string title,
            string description,
            QuestType type,
            QuestProgress progress,
            QuestReward reward,
            bool isReceived)
        {
            m_Id = id;
            m_Title = title;
            m_Description = description;
            m_Type = type;
            m_Progress = progress;
            m_Reward = reward;
            m_IsReceived = isReceived;
        }
        #endregion

        #region Methods
        public void GiveReward()
        {
            m_Reward.Give();
            m_IsReceived = true;
        }

        public void SetProgress(float value, bool isTotal)
        {
            m_Progress.SetProgress(value, isTotal);
        }
        #endregion
    }
}
