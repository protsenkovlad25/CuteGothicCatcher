using UnityEngine;

namespace CuteGothicCatcher.Core
{
    public static class PoolResources
    {
        #region Fields
        private static EntitiesConfig m_EntitiesConfig;
        #endregion

        #region Properties
        public static EntitiesConfig EntitiesConfig => m_EntitiesConfig;
        #endregion

        #region Load Methods
        public static void LoadObjects()
        {
            LoadEntitiesConfig();
        }
        private static void LoadEntitiesConfig()
        {
            m_EntitiesConfig = Resources.Load<EntitiesConfig>("Configs/Entities Config");
        }
        #endregion

        #region Get Methods
        #endregion
    }
}
