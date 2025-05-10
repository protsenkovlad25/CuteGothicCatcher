using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CuteGothicCatcher.Core
{
    [CreateAssetMenu(fileName = "Quests Config", menuName = "Configs/Quests Config")]
    public class QuestsConfig : ScriptableObject
    {
        [SerializeField] private List<QuestConfigData> m_LocalQuestDatas;
        [SerializeField] private List<QuestConfigData> m_GlobalQuestDatas;

        private Dictionary<string, QuestConfigData> m_QuestConfigDatasById;

        public List<QuestConfigData> LocalQuestDatas => m_LocalQuestDatas.Where(s => s.IsUsed).ToList();
        public List<Quest> GlobalQuestPrefabs => m_GlobalQuestDatas.Where(d => d.IsUsed).Select(d => d.QuestPrefab).ToList();
        public Dictionary<string, QuestConfigData> QuestConfigDatasById => m_QuestConfigDatasById;

        public void Init()
        {
            m_QuestConfigDatasById = new Dictionary<string, QuestConfigData>();

            foreach (var data in m_LocalQuestDatas)
            {
                m_QuestConfigDatasById.Add(data.QuestPrefab.Data.Id, data);
            }
            foreach (var data in m_GlobalQuestDatas)
            {
                m_QuestConfigDatasById.Add(data.QuestPrefab.Data.Id, data);
            }
        }

        public Quest GetQuestPrefab(string id)
        {
            return m_QuestConfigDatasById.TryGetValue(id, out var data) ? data.QuestPrefab : throw new System.NotImplementedException($"Not found quest with id - \"{id}\"");
        }

        public bool QuestUsage(string id)
        {
            return m_QuestConfigDatasById.TryGetValue(id, out var data) ? data.IsUsed : throw new System.NotImplementedException($"Not found quest with id - \"{id}\"");
        }
    }

    [System.Serializable]
    public struct QuestConfigData
    {
        [SerializeField] private Quest m_QuestPrefab;
        [SerializeField] private bool m_IsUsed;
        [SerializeField] private float m_Weight;

        public Quest QuestPrefab => m_QuestPrefab;
        public bool IsUsed => m_IsUsed;
        public float Weight => m_Weight;
    }
}
