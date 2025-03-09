using CuteGothicCatcher.Core;
using CuteGothicCatcher.Core.Interfaces;
using UnityEngine;

namespace CuteGothicCatcher.Entities.Components
{
    public class CameraAreaSpawn : MonoBehaviour, ISpawn
    {
        [SerializeField] private float m_Offset;
        [SerializeField] private Particle m_SpawnParticlePrefab;

        private Pool<Particle> m_ParticlePool;

        public void Init()
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

        public void Spawn(Transform transform)
        {
            Vector2 screenMin = MainCamera.ScreenMin + new Vector2(m_Offset, m_Offset);
            Vector2 screenMax = MainCamera.ScreenMax - new Vector2(m_Offset, m_Offset);

            float randomX = Random.Range(screenMin.x, screenMax.x);
            float randomY = Random.Range(screenMin.y, screenMax.y);

            transform.position = new Vector2(randomX, randomY);

            PlaySpawnParticle();
        }
    }
}
