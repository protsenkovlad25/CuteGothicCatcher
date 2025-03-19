using CuteGothicCatcher.Core;
using CuteGothicCatcher.Core.Interfaces;
using CuteGothicCatcher.Objects;
using System.Collections.Generic;
using UnityEngine;

namespace CuteGothicCatcher.Entities.Components
{
    public class SlowingCollision : MonoBehaviour, IColliding
    {
        [SerializeField] private List<EntityType> m_SlowingEntities;
        [SerializeField] private SlowEffectSprite m_SlowEffectSprite;
        [SerializeField] private CracksEffectSprite m_CrackEffectPrefab;
        [SerializeField] private float m_DamagePerEntity;
        [SerializeField] private float m_DamageDelay;
        [SerializeField] private float m_SlowMultiplier;

        private List<BaseEntity> m_TriggeredEntities;
        private Pool<SlowEffectSprite> m_PoolEffect;

        private CracksEffectSprite m_CrackEffect;
        private BaseEntity m_Self;

        private float m_Delay;

        public void Init(BaseEntity self)
        {
            m_Self = self;

            m_TriggeredEntities = new List<BaseEntity>();
            m_PoolEffect = new Pool<SlowEffectSprite>(m_SlowEffectSprite, 10, transform);

            m_Delay = 0;

            SetCrackEffect(self);
        }

        public void Collision(BaseEntity self, Collision2D collision)
        {
        }
        public void TriggerEnter(BaseEntity self, Collider2D collider)
        {
            if (CheckCollider(collider, out BaseEntity entity))
            {
                if (!m_TriggeredEntities.Contains(entity))
                {
                    m_TriggeredEntities.Add(entity);
                    entity.Rigidbody.velocity *= m_SlowMultiplier;

                    SetSlowEffectSprite(entity);
                    ChangeCollectability(entity, true);
                }
            }
        }
        public void TriggerExit(BaseEntity self, Collider2D collider)
        {
            if (CheckCollider(collider, out BaseEntity entity))
            {
                if (m_TriggeredEntities.Contains(entity))
                {
                    m_TriggeredEntities.Remove(entity);
                    entity.Rigidbody.velocity /= m_SlowMultiplier;

                    RemoveSlowEffectSprite(entity);
                    ChangeCollectability(entity, false);
                }
            }
        }
        public void DisableCollision()
        {
            m_CrackEffect.OnDeactivate();

            RemoveAllEffectSprite();
            m_TriggeredEntities.Clear();
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

        private void ChangeCollectability(BaseEntity entity, bool isEnter)
        {
            if (entity.Data.Clickability is CollectClickability)
            {
                CollectClickability clickability = entity.Data.Clickability as CollectClickability;
                
                if ((isEnter && clickability.CollectPoints < 0) || (!isEnter && clickability.CollectPoints >= 0))
                    clickability.SetCollectPoints(clickability.CollectPoints * -2);
            }
        }

        private void SetSlowEffectSprite(BaseEntity entity)
        {
            SlowEffectSprite sprite = m_PoolEffect.Take();

            sprite.transform.SetParent(entity.transform);
            sprite.transform.localPosition = Vector3.zero;
            sprite.transform.localRotation = Quaternion.AngleAxis(Random.Range(-180f, 180f), Vector3.forward);
        }
        private void RemoveSlowEffectSprite(BaseEntity entity)
        {
            if (entity.GetComponentInChildren<SlowEffectSprite>())
                m_PoolEffect.Return(entity.GetComponentInChildren<SlowEffectSprite>());
        }
        private void RemoveAllEffectSprite()
        {
            for (int i = m_TriggeredEntities.Count - 1; i >= 0; i--)
                TriggerExit(m_Self, m_TriggeredEntities[i].Collider);
        }

        private void CheckTriggeredEntities()
        {
            for (int i = m_TriggeredEntities.Count - 1; i >= 0; i--)
            {
                if (m_TriggeredEntities[i] == null)
                {
                    m_TriggeredEntities.RemoveAt(i);
                    continue;
                }
                if (!m_TriggeredEntities[i].gameObject.activeSelf)
                {
                    RemoveSlowEffectSprite(m_TriggeredEntities[i]);
                    ChangeCollectability(m_TriggeredEntities[i], false);
                    m_TriggeredEntities.RemoveAt(i);
                }
            }
        }
        private bool CheckCollider(Collider2D collider, out BaseEntity entity)
        {
            entity = null;
            if (collider.transform.GetComponentInParent<BaseEntity>())
            {
                entity = collider.transform.GetComponentInParent<BaseEntity>();
                return m_SlowingEntities.Contains(entity.Data.EntityType);
            }

            return false;
        }

        private void UpdateDamagePerEntity()
        {
            if (m_Delay <= 0)
            {
                float damage = m_DamagePerEntity * m_TriggeredEntities.Count;
                m_Self.TakeDamage(damage);

                m_Delay = m_DamageDelay;
            }
            else m_Delay -= Time.deltaTime;
        }

        private void Update()
        {
            CheckTriggeredEntities();
            UpdateDamagePerEntity();
        }
    }
}
