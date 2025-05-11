using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CuteGothicCatcher.Core.Controllers
{
    public class QuestsController : Controller
    {
        [SerializeField] private Transform m_GlobalQuestsParent;
        [SerializeField] private Transform m_LocalQuestsParent;

        private Dictionary<string, Quest> m_GlobalQuests;
        private Dictionary<string, Quest> m_LocalQuests;

        public override void Init()
        {
            LoadGlobalQuests();
            LoadLocalQuests();
        }

        private void LoadGlobalQuests()
        {
            m_GlobalQuests = QuestsBuilder.CreateGlobalQuests(m_GlobalQuestsParent);
        }
        private void LoadLocalQuests()
        {
            m_LocalQuests = new Dictionary<string, Quest>();
        }

        public void CreateLocalQuests(int count)
        {
            Quest newQuest;
            for (int i = 0; i < count; i++)
            {
                newQuest = QuestsBuilder.CreateRandLocalQuest(m_LocalQuestsParent, m_LocalQuests.Keys.ToList());

                m_LocalQuests.Add(newQuest.Data.Id, newQuest);
            }
        }

        public void DestroyLocalQuests()
        {
            for (int i = m_LocalQuests.Count - 1; i >= 0; i--)
                DestroyQuest(m_LocalQuests.ElementAt(i).Value);
        }
        private void DestroyQuest(Quest quest)
        {
            m_LocalQuests.Remove(quest.Data.Id);

            Destroy(quest.gameObject);
        }

        private void SaveQuests()
        {
            QuestsSaver.SaveQuests(m_GlobalQuests.Values.ToList());
        }

        public void GiveQuestRewards()
        {
            foreach (var quest in m_LocalQuests.Values)
                quest.GiveReward();
        }

        private void OnApplicationQuit()
        {
            SaveQuests();
        }
    }
}
