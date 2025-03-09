using CuteGothicCatcher.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace CuteGothicCatcher.Core.Controllers
{
    public class EntitiesController : Controller
    {
        [SerializeField] private Transform m_EntitiesParent;

        private Dictionary<EntityType, Pool<BaseEntity>> m_Entities;

        private List<BaseEntity> m_ActiveEntities;

        public override void Init()
        {
            m_Entities = new Dictionary<EntityType, Pool<BaseEntity>>();
            m_ActiveEntities = new List<BaseEntity>();

            CreateEntitiesPool(EntityType.Web, 10);
            CreateEntitiesPool(EntityType.Heart, 20);
            CreateEntitiesPool(EntityType.Scull, 20);
            CreateEntitiesPool(EntityType.Tombstone, 10);
        }

        private void CreateEntitiesPool(EntityType type, int initialCount)
        {
            if (!m_Entities.ContainsKey(type))
            {
                m_Entities.Add(type, EntityFactory.CreatePoolEntities(type, m_EntitiesParent, initialCount));
            }
        }

        public void SpawnEntities(EntityType type, int amount)
        {
            BaseEntity entity;
            for (int i = 0; i < amount; i++)
            {
                entity = m_Entities[type].Take();
                entity.StartEntity();

                m_ActiveEntities.Add(entity);
            }
        }

        public void RemoveEntities()
        {
            foreach (var entity in m_ActiveEntities)
                m_Entities[entity.Data.EntityType].Return(entity);

            m_ActiveEntities.Clear();
        }
    }
}
