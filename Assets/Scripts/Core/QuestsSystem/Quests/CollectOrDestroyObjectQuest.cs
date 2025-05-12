using CuteGothicCatcher.Entities;

namespace CuteGothicCatcher.Core
{
    public class CollectOrDestroyObjectQuest : Quest
    {
        protected override void Subscribe()
        {
            EventManager.OnCollectEntity.AddListener(Collect);
            EventManager.OnEntityDied.AddListener(Die);
        }
        protected override void Unsubscribe()
        {
            EventManager.OnCollectEntity.RemoveListener(Collect);
            EventManager.OnEntityDied.RemoveListener(Die);
        }

        private void Collect(EntityType type, float points)
        {
            UpdateProgress(1);
        }
        private void Die(BaseEntity entity)
        {
            UpdateProgress(1);
        }
    }
}
