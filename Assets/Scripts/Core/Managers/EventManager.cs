using CuteGothicCatcher.Entities;
using UnityEngine.Events;

namespace CuteGothicCatcher.Core
{
    public static class EventManager
    {
        #region Collect Events
        public static UnityEvent<EntityType, float> OnCollectEntity = new();

        public static void CollectEntity(EntityType type, float points)
        {
            OnCollectEntity?.Invoke(type, points);
        }
        #endregion

        #region Items Events
        public static UnityEvent<EntityType, int, int> OnChangedItemCount = new();

        public static void ChangedItemCount(EntityType type, int oldCount, int newCount)
        {
            OnChangedItemCount?.Invoke(type, oldCount, newCount);
        }
        #endregion

        #region Score Events
        public static UnityEvent<int> OnSetScorePoints = new();

        public static void SetScorePoints(int points)
        {
            OnSetScorePoints?.Invoke(points);
        }
        #endregion
    }
}
