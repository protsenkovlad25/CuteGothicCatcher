using CuteGothicCatcher.Core.Interfaces;
using CuteGothicCatcher.Objects;
using System.Collections.Generic;
using UnityEngine;

namespace CuteGothicCatcher.Entities.Components
{
    public class AbsorbAndCollectCollision : MonoBehaviour, IColliding
    {
        [SerializeField] private List<EntityType> m_AbsorbingEntities;
        [SerializeField] private List<EntityType> m_CollectingEntities;
        [SerializeField] private CracksEffectSprite m_CrackEffectPrefab;
        [SerializeField] private float m_DamageFromAbsorbEntity;

        private CracksEffectSprite m_CrackEffect;

        public void Init(BaseEntity self)
        {
            SetCrackEffect(self);
        }

        public void Collision(BaseEntity self, Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<BaseEntity>(out var entity))
            {
                if (m_AbsorbingEntities.Contains(entity.Data.EntityType))
                {
                    AbsorbEntity(entity);
                    self.TakeDamage(m_DamageFromAbsorbEntity);
                }

                if (m_CollectingEntities.Contains(entity.Data.EntityType))
                    CollectEntity(entity);
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
            m_CrackEffect.OnDeactivate();
        }

        private void SetCrackEffect(BaseEntity self)
        {
            m_CrackEffect = Instantiate(m_CrackEffectPrefab, transform);
            m_CrackEffect.OnDeactivate();

            self.Data.Health.OnTakedDamage += (amount) => { UpdateCracks(self.GetCurrentHealth(), self.GetMaxHealth()); };
        }
        private void UpdateCracks(float curHealth, float maxHealth)
        {
            m_CrackEffect.UpdateCracks(curHealth, maxHealth);
        }

        private void AbsorbEntity(BaseEntity entity)
        {
            entity.Die();
        }
        private void CollectEntity(BaseEntity entity)
        {
            entity.Click();
        }
    }
}
