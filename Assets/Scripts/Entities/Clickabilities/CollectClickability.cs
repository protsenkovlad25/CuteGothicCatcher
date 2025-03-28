using CuteGothicCatcher.Core;
using CuteGothicCatcher.Core.Interfaces;
using UnityEngine;

namespace CuteGothicCatcher.Entities.Components
{
    public class CollectClickability : MonoBehaviour, IClickable
    {
        public event IClickable.Clicked OnClicked;

        [SerializeField] private int m_CollectPoints;
        [SerializeField] private Particle m_PositiveCollectParticlePrefab;
        [SerializeField] private Particle m_NegativeCollectParticlePrefab;
        [SerializeField] private bool m_IsTimer;
        [SerializeField] private float m_ExtraTime;

        private Pool<Particle> m_PositivePoolParticle;
        private Pool<Particle> m_NegativePoolParticle;

        public int CollectPoints => m_CollectPoints;

        public void Init(BaseEntity self)
        {
            InitPoolsParticle();
        }

        private void InitPoolsParticle()
        {
            m_PositivePoolParticle = new Pool<Particle>(m_PositiveCollectParticlePrefab, 3, transform);
            m_NegativePoolParticle = new Pool<Particle>(m_NegativeCollectParticlePrefab, 3, transform);

            foreach (var particle in m_PositivePoolParticle.ObjectsList)
                particle.OnParticleFinished += particle.Disable;

            foreach (var particle in m_NegativePoolParticle.ObjectsList)
                particle.OnParticleFinished += particle.Disable;
        }
        private void SpawnCollectParticle(Vector3 pos)
        {
            Particle particle = m_CollectPoints < 0 ? m_NegativePoolParticle.Take() : m_PositivePoolParticle.Take();

            particle.transform.position = pos;
            particle.Play();
        }

        public void Click(BaseEntity self)
        {
            SpawnCollectParticle(self.transform.position);
            CollectEntity(self);

            self.Disable();

            OnClicked?.Invoke();
        }

        private void CollectEntity(BaseEntity entity)
        {
            if (m_IsTimer)
            {
                EventManager.SetExtraTime(m_ExtraTime);
            }

            EventManager.CollectEntity(entity.Data.EntityType, m_CollectPoints);
            EventManager.SetScorePoints(m_CollectPoints);
        }

        public void SetCollectPoints(int points)
        {
            m_CollectPoints += points;
        }

        public void DisableClickability()
        {
        }
    }
}
