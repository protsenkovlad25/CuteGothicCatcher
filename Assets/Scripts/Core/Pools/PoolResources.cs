using UnityEngine;
using CuteGothicCatcher.Entities;

namespace CuteGothicCatcher.Core
{
    public static class PoolResources
    {
        #region Fields
        private static EntitiesConfig m_EntitiesConfig;
        private static TimerGameModeConfig m_TimerGMConfig;
        private static StoryGameModeConfig m_StoryGMConfig;
        private static LevelsGameModeConfig m_LevelsGMConfig;
        private static QuestsConfig m_QuestsConfig;
        #endregion

        #region Properties
        public static EntitiesConfig EntitiesConfig => m_EntitiesConfig;
        public static TimerGameModeConfig TimerGMConfig => m_TimerGMConfig;
        public static StoryGameModeConfig StoryGMConfig => m_StoryGMConfig;
        public static LevelsGameModeConfig LevelsGMConfig => m_LevelsGMConfig;
        public static QuestsConfig QuestsConfig => m_QuestsConfig;
        #endregion

        #region Load Methods
        public static void LoadAll()
        {
            LoadConfigs();
            LoadObjects();
        }
        private static void LoadConfigs()
        {
            m_EntitiesConfig = Resources.Load<EntitiesConfig>("Configs/Entities Config");

            m_TimerGMConfig = Resources.Load<TimerGameModeConfig>("Configs/TimerGameMode Config");
            m_StoryGMConfig = Resources.Load<StoryGameModeConfig>("Configs/StoryGameMode Config");
            m_LevelsGMConfig = Resources.Load<LevelsGameModeConfig>("Configs/LevelsGameMode Config");

            m_QuestsConfig = Resources.Load<QuestsConfig>("Configs/Quests Config");
            m_QuestsConfig.Init();
        }
        private static void LoadObjects()
        {

        }
        #endregion
    }
}
