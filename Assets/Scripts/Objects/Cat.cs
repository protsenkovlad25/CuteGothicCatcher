using CuteGothicCatcher.Core;
using CuteGothicCatcher.Core.Interfaces;
using UnityEngine;

namespace CuteGothicCatcher.Objects
{
    public class Cat : MonoBehaviour, IIniting
    {
        [Header("Particles Data")]
        [SerializeField] private Transform m_ParticlesParent;
        [Space]
        [SerializeField] private Particle m_HeartParticlePrefab;
        [SerializeField] private Particle m_MeowParticlePrefab;
        [SerializeField] private Particle m_LoveParticlePrefab;
        [Space]
        [SerializeField] private int m_HeartsInitCount;
        [SerializeField] private int m_MeowsInitCount;
        [SerializeField] private int m_LovesInitCount;

        [Header("Percents")]
        [SerializeField] private float m_HeartPercent;
        [SerializeField] private float m_LovePercent;

        private Pool<Particle> m_HeartsPool;
        private Pool<Particle> m_MeowsPool;
        private Pool<Particle> m_LovesPool;

        public void Init()
        {
            InitPools();
        }

        private void InitPools()
        {
            m_HeartsPool = new Pool<Particle>(m_HeartParticlePrefab, m_HeartsInitCount, m_ParticlesParent);
            m_MeowsPool = new Pool<Particle>(m_MeowParticlePrefab, m_MeowsInitCount, m_ParticlesParent);
            m_LovesPool = new Pool<Particle>(m_LoveParticlePrefab, m_LovesInitCount, m_ParticlesParent);

            foreach (var particle in m_HeartsPool.Objects)
                InitPoolParticle(particle.Key);

            foreach (var particle in m_MeowsPool.Objects)
                InitPoolParticle(particle.Key);

            foreach (var particle in m_LovesPool.Objects)
                InitPoolParticle(particle.Key);
        }
        private void InitPoolParticle(Particle particle)
        {
            particle.OnParticleFinished += particle.Disable;
            particle.transform.localPosition = Vector3.zero;
            particle.transform.localScale = Vector3.one;
        }

        private void SpawnParticle()
        {
            Particle particle = GetRandomParticle();

            particle.transform.SetParent(m_ParticlesParent);
            particle.transform.localPosition = Vector3.zero;
            particle.transform.localScale = Vector3.one;

            particle.Play();
        }

        private Particle GetRandomParticle()
        {
            return Random.Range(0f, 100f) <= m_LovePercent ? m_LovesPool.Take() : 
                   (Random.Range(0f, 100f) <= m_HeartPercent ? m_HeartsPool.Take() : m_MeowsPool.Take());
        }

        private void OnMouseDown()
        {
            SpawnParticle();
        }
    }
}
