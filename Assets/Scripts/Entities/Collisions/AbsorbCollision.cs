using CuteGothicCatcher.Core.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace CuteGothicCatcher.Entities.Components
{
    public class AbsorbCollision : MonoBehaviour, IColliding
    {
        [SerializeField] private List<EntityType> m_AbsorbingEntities;

        public void Init(BaseEntity self)
        {
        }

        public void Collision(BaseEntity self, Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<BaseEntity>(out var entity))
            {
                if (m_AbsorbingEntities.Contains(entity.Data.EntityType))
                    entity.Die();
            }
        }

        public void TriggerEnter(BaseEntity self, Collider2D collider)
        {
        }

        public void TriggerExit(BaseEntity self, Collider2D collider)
        {
        }

        public void DisableCollision()
        {
        }
    }
}
