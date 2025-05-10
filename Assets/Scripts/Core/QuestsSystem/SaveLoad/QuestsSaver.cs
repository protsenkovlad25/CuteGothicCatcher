using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CuteGothicCatcher.Core
{
    public static class QuestsSaver
    {
        public static void SaveQuests(List<Quest> quests)
        {
            Save(quests.Select(q => q.Data).ToList());
        }

        public static void SaveQuestsFromConfig()
        {
            Save(PoolResources.QuestsConfig.GlobalQuestPrefabs.Select(p => p.Data).ToList());
        }

        private static void Save(List<QuestData> questDatas)
        {
            string jsonFile = JsonConvert.SerializeObject(questDatas);
            File.WriteAllText(QuestsLoader.FilePath, jsonFile);
        }
    }
}
