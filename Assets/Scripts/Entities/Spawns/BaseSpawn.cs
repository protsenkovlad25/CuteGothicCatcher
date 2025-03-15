using CuteGothicCatcher.Core;
using CuteGothicCatcher.Core.Interfaces;
using UnityEngine;

namespace CuteGothicCatcher.Entities.Components
{
    public class BaseSpawn : MonoBehaviour, ISpawn
    {
        [SerializeField] private Particle m_SpawnParticlePrefab;

        private Pool<Particle> m_ParticlePool;

        public virtual void Init()
        {
            InitSpawnParticle();
        }

        private void InitSpawnParticle()
        {
            if (m_SpawnParticlePrefab)
            {
                m_ParticlePool = new Pool<Particle>(m_SpawnParticlePrefab, 1, transform);

                foreach (var particle in m_ParticlePool.ObjectsList)
                    particle.OnParticleFinished += () => { m_ParticlePool.Return(particle); };
            }
        }

        private void PlaySpawnParticle()
        {
            if (m_ParticlePool != null)
            {
                Particle particle = m_ParticlePool.Take();

                particle.transform.position = transform.position;
                particle.Play();
            }
        }

        public virtual void Spawn(Transform transform)
        {
            PlaySpawnParticle();
        }
    }
}
