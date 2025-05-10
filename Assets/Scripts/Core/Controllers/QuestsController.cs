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

            CreateLocalQuests(2);
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
            Quest quest;
            for (int i = 0; i < count; i++)
            {
                quest = QuestsBuilder.CreateRandLocalQuest(m_LocalQuestsParent, m_LocalQuests.Keys.ToList());

                m_LocalQuests.Add(quest.Data.Id, quest);
            }
        }
    }
}
