using CuteGothicCatcher.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CuteGothicCatcher.Core.Controllers
{
    public class QuestsController : Controller
    {
        [Header("Parents")]
        [SerializeField] private Transform m_GlobalQuestsParent;
        [SerializeField] private Transform m_LocalQuestsParent;

        [Header("Panels")]
        [SerializeField] private QuestCheckMarksPanel m_QuestCheckMarksPanel;
        [SerializeField] private GlobalQuestsPanel m_GlobalQuestsPanel;
        [SerializeField] private LocalQuestsPanel m_LocalQuestsPanel;

        private List<Quest> m_GlobalQuests;
        private List<Quest> m_LocalQuests;

        #region Methods
        public override void Init()
        {
            LoadGlobalQuests();
            LoadLocalQuests();

            m_GlobalQuestsPanel.OnGiveQuestReward = GiveQuestReward;
            m_GlobalQuestsPanel.InitSlots(m_GlobalQuests);

            m_QuestCheckMarksPanel.Init();
        }

        #region Save&Load
        private void LoadGlobalQuests()
        {
            m_GlobalQuests = QuestsBuilder.CreateGlobalQuests(m_GlobalQuestsParent);
        }
        private void LoadLocalQuests()
        {
            m_LocalQuests = new List<Quest>();
        }
        private void SaveQuests()
        {
            QuestsSaver.SaveQuests(m_GlobalQuests);
        }
        #endregion

        public void CreateLocalQuests(int count)
        {
            Quest newQuest;
            for (int i = 0; i < count; i++)
            {
                newQuest = QuestsBuilder.CreateRandLocalQuest(m_LocalQuestsParent, m_LocalQuests.Select(s => s.Data.Id).ToList());

                m_LocalQuests.Add(newQuest);
            }

            InitPanelSlots();
        }

        private void InitPanelSlots()
        {
            m_QuestCheckMarksPanel.InitSlots(m_LocalQuests);
            m_LocalQuestsPanel.InitSlots(m_LocalQuests);
        }

        public void DestroyLocalQuests()
        {
            for (int i = m_LocalQuests.Count - 1; i >= 0; i--)
                DestroyQuest(m_LocalQuests[i]);

            m_QuestCheckMarksPanel.DestroySlots();
            m_LocalQuestsPanel.DestroySlots();
        }
        private void DestroyQuest(Quest quest)
        {
            m_LocalQuests.Remove(quest);

            Destroy(quest.gameObject);
        }

        public void GiveQuestReward(Quest quest)
        {
            quest.GiveReward();
        }
        public void GiveQuestRewards()
        {
            foreach (var quest in m_LocalQuests)
                quest.GiveReward();
        }

        private void OnApplicationQuit()
        {
            SaveQuests();
        }
        #endregion
    }
}
