using System.Collections.Generic;
using UnityEngine;

namespace CuteGothicCatcher.Core
{
    [CreateAssetMenu(fileName = "Quests Config", menuName = "Configs/Quests Config")]
    public class QuestsConfig : ScriptableObject
    {
        [SerializeField] private List<Quest> m_LocalQuestPrefabs;
        [SerializeField] private List<Quest> m_GlobalQuestPrefabs;

        public List<Quest> LocalQuestPrefabs => m_LocalQuestPrefabs;
        public List<Quest> GlobalQuestPrefabs => m_GlobalQuestPrefabs;
        public Dictionary<string, Quest> m_QuestsPrefabsById;

        public void Init()
        {
            m_QuestsPrefabsById = new Dictionary<string, Quest>();

            foreach (var q in m_LocalQuestPrefabs)
            {
                m_QuestsPrefabsById.Add(q.Data.Id, q);
            }
            foreach (var q in m_GlobalQuestPrefabs)
            {
                m_QuestsPrefabsById.Add(q.Data.Id, q);
            }
        }

        public Quest GetQuest(string id)
        {
            return m_QuestsPrefabsById.TryGetValue(id, out var quest) ? quest : throw new System.NotImplementedException($"Not found quest with id - \"{id}\"");
        }
    }
}
