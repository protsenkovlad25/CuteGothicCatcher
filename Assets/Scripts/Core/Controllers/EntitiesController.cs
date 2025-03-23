using CuteGothicCatcher.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace CuteGothicCatcher.Core.Controllers
{
    public class EntitiesController : Controller
    {
        [SerializeField] private Transform m_EntitiesParent;

        private Dictionary<(EntityType, EntitySubType), Pool<BaseEntity>> m_Entities;

        private List<BaseEntity> m_ActiveEntities;

        public override void Init()
        {
            m_Entities = new Dictionary<(EntityType, EntitySubType), Pool<BaseEntity>>();
            m_ActiveEntities = new List<BaseEntity>();

            CreateEntitiesPool(EntityType.Heart, 20);
            CreateEntitiesPool(EntityType.Heart, EntitySubType.Timer, 10);
            
            CreateEntitiesPool(EntityType.Scull, 20);
            CreateEntitiesPool(EntityType.Scull, EntitySubType.Timer, 10);
            
            CreateEntitiesPool(EntityType.Web, 10);
            CreateEntitiesPool(EntityType.Kitty, 20);
            CreateEntitiesPool(EntityType.Tombstone, 10);
        }

        private void CreateEntitiesPool(EntityType type, int initialCount)
        {
            if (!m_Entities.ContainsKey((type, EntitySubType.Ordinary)))
            {
                m_Entities.Add((type, EntitySubType.Ordinary), EntityFactory.CreatePoolEntities(type, EntitySubType.Ordinary, m_EntitiesParent, initialCount));
            }
        }
        private void CreateEntitiesPool(EntityType type, EntitySubType subType, int initialCount)
        {
            if (!m_Entities.ContainsKey((type, subType)))
            {
                m_Entities.Add((type, subType), EntityFactory.CreatePoolEntities(type, subType, m_EntitiesParent, initialCount));
            }
        }

        public void SpawnEntity(EntityType type, EntitySubType subType = EntitySubType.Ordinary)
        {
            BaseEntity entity;

            entity = m_Entities[(type, subType)].Take();
            entity.StartEntity();

            m_ActiveEntities.Add(entity);
        }
        public void SpawnEntity(EntityType type, Vector2 position)
        {
            BaseEntity entity;

            entity = m_Entities[(type, EntitySubType.Ordinary)].Take();
            entity.transform.position = position;
            entity.StartEntity();

            m_ActiveEntities.Add(entity);
        }
        public void SpawnEntities(EntityType type, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                SpawnEntity(type);
            }
        }

        public void RemoveEntities()
        {
            foreach (var entity in m_ActiveEntities)
                m_Entities[(entity.Data.EntityType, entity.Data.EntitySubType)].Return(entity);

            m_ActiveEntities.Clear();
        }
    }
}
