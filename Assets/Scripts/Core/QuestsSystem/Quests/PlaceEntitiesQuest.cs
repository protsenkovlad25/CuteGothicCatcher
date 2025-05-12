using CuteGothicCatcher.Entities;
using UnityEngine;

namespace CuteGothicCatcher.Core
{
    [System.Serializable]
    public class PlaceEntitiesQuest : Quest
    {
        [SerializeField] private EntityType m_EntityType;

        protected override void Subscribe()
        {
            EventManager.OnPlacedItem.AddListener(PlacedItem);
        }

        protected override void Unsubscribe()
        {
            EventManager.OnPlacedItem.RemoveListener(PlacedItem);
        }

        private void PlacedItem(EntityType type)
        {
            if (type == m_EntityType)
                UpdateProgress(1);
        }
    }
}
