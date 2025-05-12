using CuteGothicCatcher.Entities;
using UnityEngine;

namespace CuteGothicCatcher.Core
{
    [System.Serializable]
    public class KillEntitiesQuest : Quest
    {
        [SerializeField] private EntityType m_EntityType;

        protected override void Subscribe()
        {
            EventManager.OnEntityDied.AddListener(EntityDied);
        }

        protected override void Unsubscribe()
        {
            EventManager.OnEntityDied.RemoveListener(EntityDied);
        }

        private void EntityDied(BaseEntity entity)
        {
            if (entity.Data.EntityType == m_EntityType)
                UpdateProgress(1);
        }
    }
}
