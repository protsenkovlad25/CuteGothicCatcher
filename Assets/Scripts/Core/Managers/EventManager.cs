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
        public static UnityEvent<EntityType> OnPlacedItem = new();

        public static void ChangedItemCount(EntityType type, int oldCount, int newCount)
        {
            OnChangedItemCount?.Invoke(type, oldCount, newCount);
        }
        public static void PlacedItem(EntityType type)
        {
            OnPlacedItem?.Invoke(type);
        }
        #endregion

        #region Score Events
        public static UnityEvent<int> OnSetScorePoints = new();

        public static void SetScorePoints(int points)
        {
            OnSetScorePoints?.Invoke(points);
        }
        #endregion

        #region Entities Events
        public static UnityEvent<BaseEntity> OnEntityDied = new UnityEvent<BaseEntity>();

        public static void EntityDied(BaseEntity entity)
        {
            OnEntityDied?.Invoke(entity);
        }
        #endregion

        #region Timer Events
        public static UnityEvent<float> OnSetExtraTime = new UnityEvent<float>();

        public static void SetExtraTime(float time)
        {
            OnSetExtraTime?.Invoke(time);
        }
        #endregion

        #region Quests Events
        public static UnityEvent<string> OnCompletedQuest = new();

        public static void CompleteQuest(string id)
        {
            OnCompletedQuest?.Invoke(id);
        }
        #endregion

        #region Objects Events
        public static UnityEvent OnCatClick = new();

        public static void ClickCat()
        {
            OnCatClick?.Invoke();
        }
        #endregion
    }
}
