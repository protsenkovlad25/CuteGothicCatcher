using CuteGothicCatcher.Core;
using CuteGothicCatcher.Core.Interfaces;
using UnityEngine;

namespace CuteGothicCatcher.Entities.Components
{
    public class Health : MonoBehaviour, IHealth
    {
        public event IHealth.TakedDamage OnTakedDamage;
        public event IHealth.Healed OnHealed;
        public event IHealth.Died OnDie;

        [Header("Health Data")]
        [SerializeField] private float m_MaxHealth;
        [SerializeField] private float m_StartHealth;
        [SerializeField] private Particle m_DieParticlePrefab;
        [Header("Score")]
        [SerializeField] private int m_DiePoints;
        [SerializeField] private int m_HealPoints;
        [SerializeField] private int m_TakeDamagePoints;

        private float m_CurrentHealth;
        private Pool<Particle> m_ParticlePool;

        public void Init()
        {
            ResetHealth();
            InitParticlePool();
        }

        private void InitParticlePool()
        {
            if (m_DieParticlePrefab)
            {
                m_ParticlePool = new Pool<Particle>(m_DieParticlePrefab, 1, transform);

                foreach (var particle in m_ParticlePool.ObjectsList)
                    particle.OnParticleFinished += () => { m_ParticlePool.Return(particle); };
            }
        }

        private void PlayDieParticle()
        {
            if (m_ParticlePool != null)
            {
                Particle particle = m_ParticlePool.Take();

                particle.transform.position = transform.position;
                particle.Play();
            }
        }

        public void ResetHealth()
        {
            m_CurrentHealth = m_StartHealth;
        }

        public void Heal(float amount)
        {
            OnHealed?.Invoke(amount);
            EventManager.SetScorePoints(m_HealPoints);
        }

        public void TakeDamage(float amount)
        {
            m_CurrentHealth -= amount;

            if (m_CurrentHealth < 0)
            {
                Die();
            }
            else
            {
                OnTakedDamage?.Invoke(amount);
                EventManager.SetScorePoints(m_TakeDamagePoints);
            }
        }

        public void Die()
        {
            PlayDieParticle();

            OnDie?.Invoke();
            EventManager.SetScorePoints(m_DiePoints);
        }

        public float GetMaxHealth()
        {
            return m_MaxHealth;
        }

        public float GetCurrentHealth()
        {
            return m_CurrentHealth;
        }
    }
}