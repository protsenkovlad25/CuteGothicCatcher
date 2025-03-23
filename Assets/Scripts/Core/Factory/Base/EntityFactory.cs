using CuteGothicCatcher.Entities;
using UnityEngine;

namespace CuteGothicCatcher.Core
{
    public static class EntityFactory
    {
        public static BaseEntity CreateEntity(EntityType type, EntitySubType subType, Transform parent)
        {
            EntityData data = PoolResources.EntitiesConfig.GetEntityData(type, subType);
            BaseEntity entity = Object.Instantiate(data.Prefab, parent);

            entity.SetData(GetNewEntityData(type, subType, entity.transform));
            entity.Init();

            return entity;
        }

        public static Pool<BaseEntity> CreatePoolEntities(EntityType type, EntitySubType subType, Transform parent, int initialCount)
        {
            EntityData data = PoolResources.EntitiesConfig.GetEntityData(type, subType);

            Pool<BaseEntity> pool = new Pool<BaseEntity>(data.Prefab, initialCount, parent);
            pool.OnCreateNew += (entity) =>
            {
                SetDataOfPoolEntity(entity, type, subType, $"{type}.{subType}_{pool.NewObjects.Count}_New", pool);
            };

            for (int i = 0; i < pool.ObjectsList.Count; i++)
            {
                SetDataOfPoolEntity(pool.ObjectsList[i], type, subType, $"{type}.{subType}_{i}", pool);
            }

            return pool;
        }

        private static void SetDataOfPoolEntity(BaseEntity entity, EntityType type, EntitySubType subType, string name, Pool<BaseEntity> pool)
        {
            EntityData newData = GetNewEntityData(type, subType, entity.transform);
            newData.Health.OnDie += () =>
            {
                EventManager.EntityDied(entity);
                pool.Return(entity);
            };

            entity.name = name;
            entity.SetData(newData);
            entity.Init();
        }

        private static EntityData GetNewEntityData(EntityType type, EntitySubType subType, Transform entity)
        {
            EntityData data = PoolResources.EntitiesConfig.GetEntityData(type, subType);

            EntityData newData = new EntityData()
            {
                TextId = data.TextId,
                EntityType = type,
                EntitySubType = subType,
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
