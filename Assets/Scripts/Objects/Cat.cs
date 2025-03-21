using CuteGothicCatcher.Core;
using CuteGothicCatcher.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CuteGothicCatcher.Objects
{
    public class Cat : MonoBehaviour, IIniting
    {
        [System.Serializable]
        private class ParticleData
        {
            [SerializeField] private Particle m_Prefab;
            [SerializeField] private int m_InitCount;
            [SerializeField] private float m_Percent;

            public Particle Prefab => m_Prefab;
            public int InitCount => m_InitCount;
            public float Percent => m_Percent;
        }

        [SerializeField] private Transform m_ParticlesParent;
        [SerializeField] private List<ParticleData> m_ParticlesData;

        private List<(ParticleData, Pool<Particle>)> m_ParticlePools;

        public void Init()
        {
            InitPools();
        }

        private void InitPools()
        {
            m_ParticlePools = new List<(ParticleData, Pool<Particle>)>();

            Pool<Particle> pool;
            foreach (var data in m_ParticlesData)
            {
                pool = new Pool<Particle>(data.Prefab, data.InitCount, m_ParticlesParent);
                pool.OnCreateNew = InitPoolParticle;

                foreach (var particle in pool.Objects)
                    InitPoolParticle(particle.Key);

                m_ParticlePools.Add((data, pool));
            }
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
            float totalWeight = m_ParticlesData.Sum(d => d.Percent);
            float randValue = Random.Range(0f, totalWeight);
            float cumulativeWeight = 0;

            foreach (var item in m_ParticlePools)
            {
                cumulativeWeight += item.Item1.Percent;
                if (randValue < cumulativeWeight)
                    return item.Item2.Take();
            }

            throw new System.Exception("Couldn't get a random particle");
        }

        private void OnMouseDown()
        {
            SpawnParticle();
        }
    }
}
