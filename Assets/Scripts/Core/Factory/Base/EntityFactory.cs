using CuteGothicCatcher.Entities;
using UnityEngine;

namespace CuteGothicCatcher.Core
{
    public static class EntityFactory
    {
        public static BaseEntity CreateEntity(EntityType type, Transform parent)
        {
            EntityData data = PoolResources.EntitiesConfig.GetEntityData(type);
            BaseEntity entity = Object.Instantiate(data.Prefab, parent);

            entity.SetData(GetNewEntityData(type, entity.transform));
            entity.Init();

            return entity;
        }

        public static Pool<BaseEntity> CreatePoolEntities(EntityType type, Transform parent, int initialCount)
        {
            EntityData data = PoolResources.EntitiesConfig.GetEntityData(type);

            Pool<BaseEntity> pool = new Pool<BaseEntity>(data.Prefab, initialCount, parent);
            pool.OnCreateNew += (entity) =>
            {
                SetDataOfPoolEntity(entity, type, $"{type}_{pool.NewObjects.Count}_New", pool);
            };

            for (int i = 0; i < pool.ObjectsList.Count; i++)
            {
                SetDataOfPoolEntity(pool.ObjectsList[i], type, $"{type}_{i}", pool);
            }

            return pool;
        }

        private static void SetDataOfPoolEntity(BaseEntity entity, EntityType type, string name, Pool<BaseEntity> pool)
        {
            EntityData newData = GetNewEntityData(type, entity.transform);
            newData.Health.OnDie += () => { pool.Return(entity); };

            entity.name = name;
            entity.SetData(newData);
            entity.Init();
        }

        private static EntityData GetNewEntityData(EntityType type, Transform entity)
        {
            EntityData data = PoolResources.EntitiesConfig.GetEntityData(type);

            EntityData newData = new EntityData()
            {
                TextId = data.TextId,
                EntityType = type,
                Sprite = data.Sprite,
                SpawnMB = Object.Instantiate(data.SpawnMB, entity),
                HealthMB = Object.Instantiate(data.HealthMB, entity),
                MovementMB = Object.Instantiate(data.MovementMB, entity),
                CollisionMB = Object.Instantiate(data.CollisionMB, entity),
                ClickabilityMB = Object.Instantiate(data.ClickabilityMB, entity)
            };

            return newData;
        }
    }
}
