using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using CuteGothicCatcher.Core.Player;

namespace CuteGothicCatcher.Core.Controllers
{
    public static class PlayerController
    {
        private static readonly string m_FilePath = $"{Application.persistentDataPath}/PlayerData.json";

        private static PlayerData m_PlayerData;

        public static PlayerData PlayerData => m_PlayerData;

        public static void Init()
        {
            LoadPlayer();
        }

        public static void NewPlayer()
        {
            m_PlayerData = new PlayerData();
            SavePlayer();
        }

        public static void LoadPlayer()
        {
            if (File.Exists(m_FilePath))
            {
                string jsonFile = File.ReadAllText(m_FilePath);

                m_PlayerData = JsonConvert.DeserializeObject<PlayerData>(jsonFile);
                m_PlayerData.Load();
            }
            else NewPlayer();
        }
        public static void SavePlayer()
        {
            string jsonFile = JsonConvert.SerializeObject(m_PlayerData);
            File.WriteAllText(m_FilePath, jsonFile);
        }
    }
}
