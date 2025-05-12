using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace CuteGothicCatcher.Core
{
    public static class QuestsLoader
    {
        public static readonly string FilePath = $"{Application.persistentDataPath}/QuestsData.json";

        public static List<QuestData> LoadQuestDatas()
        {
            if (!File.Exists(FilePath))
            {
                QuestsSaver.SaveQuestsFromConfig();
            }

            return Load();
        }

        private static List<QuestData> Load()
        {
            string jsonFile = File.ReadAllText(FilePath);

            return JsonConvert.DeserializeObject<List<QuestData>>(jsonFile);
        }
    }
}
