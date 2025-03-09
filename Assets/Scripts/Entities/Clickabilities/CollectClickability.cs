using CuteGothicCatcher.Core;
using CuteGothicCatcher.Core.Interfaces;
using UnityEngine;

namespace CuteGothicCatcher.Entities.Components
{
    public class CollectClickability : MonoBehaviour, IClickable
    {
        [SerializeField] private float m_CollectPoints;
        [SerializeField] private Particle m_CollectParticlePrefab;

        private Pool<Particle> m_PoolParticle;

        public void Init(BaseEntity self)
        {
            InitPoolParticle();
        }

        private void InitPoolParticle()
        {
            m_PoolParticle = new Pool<Particle>(m_CollectParticlePrefab, 3, transform);

            foreach (var particle in m_PoolParticle.ObjectsList)
                particle.OnParticleFinished += particle.Disable;
        }
        private void SetCollectParticle(Vector3 pos)
        {
            Particle particle = m_PoolParticle.Take();

            particle.transform.position = pos;
            particle.Play();
        }

        public void Click(BaseEntity self)
        {
            SetCollectParticle(self.transform.position);

            self.Disable();
        }

        public void DisableClickability()
        {
        }
    }
}
