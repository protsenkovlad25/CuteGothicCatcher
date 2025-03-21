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
        #endregion

        #region Properties
        public static EntitiesConfig EntitiesConfig => m_EntitiesConfig;
        public static TimerGameModeConfig TimerGMConfig => m_TimerGMConfig;
        public static StoryGameModeConfig StoryGMConfig => m_StoryGMConfig;
        public static LevelsGameModeConfig LevelsGMConfig => m_LevelsGMConfig;
        #endregion

        #region Load Methods
        public static void LoadObjects()
        {
            LoadEntitiesConfig();
            LoadGameModeConfigs();
        }
        private static void LoadEntitiesConfig()
        {
            m_EntitiesConfig = Resources.Load<EntitiesConfig>("Configs/Entities Config");
        }
        private static void LoadGameModeConfigs()
        {
            m_TimerGMConfig = Resources.Load<TimerGameModeConfig>("Configs/TimerGameMode Config");
            m_StoryGMConfig = Resources.Load<StoryGameModeConfig>("Configs/StoryGameMode Config");
            m_LevelsGMConfig = Resources.Load<LevelsGameModeConfig>("Configs/LevelsGameMode Config");
        }
        #endregion

        #region Get Methods
        #endregion
    }
}
