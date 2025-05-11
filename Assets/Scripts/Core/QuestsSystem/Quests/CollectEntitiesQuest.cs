using CuteGothicCatcher.Entities;
using UnityEngine;

namespace CuteGothicCatcher.Core
{
    [System.Serializable]
    public class CollectEntitiesQuest : Quest
    {
        [SerializeField] private EntityType m_EntityType;

        protected override void Subscribe()
        {
            EventManager.OnCollectEntity.AddListener(CollectEntity);
        }

        protected override void Unsubscribe()
        {
            EventManager.OnCollectEntity.RemoveListener(CollectEntity);
        }

        private void CollectEntity(EntityType type, float scorePoints)
        {
            if (type == m_EntityType)
                UpdateProgress(1);
        }
    }
}
