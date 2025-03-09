using CuteGothicCatcher.Core;
using CuteGothicCatcher.Core.Interfaces;
using UnityEngine;

namespace CuteGothicCatcher.Entities
{
    public class BaseEntity : MonoBehaviour, IIniting, IPoolable, IDamageable
    {
        #region SerializeFields
        [SerializeField] private Rigidbody2D m_Rigidbody;
        [SerializeField] private Collider2D m_LogicCollider;
        [SerializeField] private Collider2D m_PhysicCollider;
        #endregion

        #region Fields
        protected EntityData m_Data;

        protected bool m_IsActive;
        #endregion

        #region Properties
        public EntityData Data => m_Data;
        public Rigidbody2D Rigidbody => m_Rigidbody;
        public Collider2D Collider => m_LogicCollider;
        #endregion

        #region Methods
        public void Init()
        {
            m_Data.Spawn.Init();
            m_Data.Health.Init();
            m_Data.Movement.Init(m_Rigidbody, transform);
            m_Data.Collision.Init(this);
            m_Data.Clickability.Init(this);

            GameManager.OnTimeScaleChanged.AddListener(CheckTimeScale);
        }

        public void SetData(EntityData data)
        {
            m_Data = data;
        }

        public virtual void StartEntity()
        {
            m_IsActive = true;

            m_LogicCollider.enabled = false;

            Spawn();
            StartMove();
        }

        private void CheckTimeScale(float timeScale)
        {
            ChangeColliderState(timeScale != 0);
        }

        public void ChangeColliderState(bool state)
        {
            /*if (m_Collider)
            {
                Sequence s = DOTween.Sequence();

                s.AppendInterval(1f);
                s.AppendCallback(() => { m_Collider.enabled = state; });
            }*/

            m_LogicCollider.enabled = state;
        }

        #region Health
        public void TakeDamage(float amount)
        {
            m_Data.Health.TakeDamage(amount);
        }
        public float GetCurrentHealth()
        {
            return m_Data.Health.GetCurrentHealth();
        }
        public float GetMaxHealth()
        {
            return m_Data.Health.GetMaxHealth();
        }
        public void Die()
        {
            m_Data.Health.Die();
        }
        #endregion

        #region Spawn
        protected virtual void Spawn()
        {
            m_Data.Spawn?.Spawn(transform);
        }
        #endregion

        #region Move
        protected virtual void Move()
        {
            m_Data.Movement?.Move();
        }
        public void StartMove()
        {
            m_Data.Movement?.StartMove();
        }
        public void StopMove()
        {
            m_Data.Movement?.StopMove();
        }
        #endregion

        #region Poolable
        public virtual void OnActivate()
        {
            m_Data.Health.ResetHealth();
        }
        public virtual void OnDeactivate()
        {
            m_Data.Movement?.DisableMove();
            m_Data.Collision?.DisableCollision();
            m_Data.Clickability?.DisableClickability();
        }
        #endregion

        #region Unity
        private void OnMouseDown()
        {
            m_Data.Clickability.Click(this);
        }
        protected void OnCollisionEnter2D(Collision2D collision)
        {
            //Debug.Log($"Collision {name} - {collision.gameObject.name}");
            m_Data.Collision?.Collision(this, collision);
        }
        protected void OnTriggerEnter2D(Collider2D collider)
        {
            //Debug.Log($"Trigger Enter {name} - {collider.gameObject.name}");
            m_Data.Collision?.TriggerEnter(this, collider);
        }
        protected void OnTriggerExit2D(Collider2D collider)
        {
            //Debug.Log($"Trigger Exit {name} - {collider.gameObject.name}");
            m_Data.Collision?.TriggerExit(this, collider);
        }
        protected void FixedUpdate()
        {
            Move();
        }
        #endregion
        #endregion
    }
}
