using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CuteGothicCatcher.Core
{
    public static class QuestsBuilder
    {
        private static QuestsConfig m_Config => PoolResources.QuestsConfig;

        public static Dictionary<string, Quest> CreateGlobalQuests(Transform parent)
        {
            Dictionary<string, Quest> globalQuests = new Dictionary<string, Quest>();
            List<QuestData> questDatas = QuestsLoader.LoadQuestDatas();

            QuestData newData;
            foreach (var data in questDatas)
            {
                if (m_Config.QuestUsage(data.Id))
                {
                    newData = CreateQuestData(data);

                    globalQuests.Add(newData.Id, CreateQuest(m_Config.GetQuestPrefab(newData.Id), newData, parent));
                }
            }

            return globalQuests;
        }

        private static QuestData CreateQuestData(QuestData data)
        {
            QuestData configData = m_Config.GetQuestPrefab(data.Id).Data;

            QuestData newData = new QuestData(
                data.Id,
                configData.Title,
                configData.Description,
                data.Type,
                new QuestProgress(data.Progress.Current, configData.Progress.Required),
                new QuestReward(configData.Reward.Type, configData.Reward.Value),
                data.IsReceived);

            return newData;
        }

        private static Quest CreateQuest(Quest prefab, QuestData data, Transform parent)
        {
            Quest newQuest = Object.Instantiate(prefab, parent);

            newQuest.name = newQuest.name.Replace("(Clone)", "");
            newQuest.SetaData(data);
            newQuest.Init();

            return newQuest;
        }

        public static Quest CreateRandLocalQuest(Transform parent, List<string> exceptions)
        {
            List<QuestConfigData> configDatas = new List<QuestConfigData>();
            configDatas.AddRange(m_Config.LocalQuestDatas);
            configDatas.RemoveAll(s => exceptions.Contains(s.QuestPrefab.Data.Id));

            float totalWeight = configDatas.Sum(s => s.Weight);
            float randValue = Random.Range(0f, totalWeight);
            float cumulativeWeight = 0;

            Quest randQuest = null;
            foreach (var data in configDatas)
            {
                cumulativeWeight += data.Weight;
                if (randValue < cumulativeWeight)
                {
                    randQuest = data.QuestPrefab;
                    break;
                }
            }

            return CreateQuest(randQuest, new QuestData(randQuest.Data), parent);
        }
    }
}
