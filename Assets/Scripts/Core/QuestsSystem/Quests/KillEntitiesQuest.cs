using CuteGothicCatcher.Entities;
using UnityEngine;

namespace CuteGothicCatcher.Core
{
    [System.Serializable]
    public class KillEntitiesQuest : Quest
    {
        [SerializeField] private EntityType m_EntityType;

        public EntityType EntityType => m_EntityType;

        public override void Init()
        {
            base.Init();
        }

        public override void UpdateProgress(int value)
        {
            base.UpdateProgress(value);
        }
    }
}
