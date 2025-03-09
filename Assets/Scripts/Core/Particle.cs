using CuteGothicCatcher.Core.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace CuteGothicCatcher.Core
{
    public class Particle : MonoBehaviour, IPoolable
    {
        public System.Action OnParticleFinished;

        [SerializeField] private List<ParticleSystem> m_Particles;

        private bool m_IsPlaying;

        public void Play()
        {
            foreach (var particle in m_Particles)
                particle.Play();

            m_IsPlaying = true;
            enabled = true;
        }
        public void Pause()
        {
            foreach (var particle in m_Particles)
                particle.Pause();
        }
        public void Stop()
        {
            foreach (var particle in m_Particles)
                particle.Stop();

            m_IsPlaying = false;
            enabled = false;
        }

        public void OnActivate()
        {
        }
        public void OnDeactivate()
        {
        }

        private void Update()
        {
            if (!m_IsPlaying) return;

            bool allStopped = true;
            foreach (var particle in m_Particles)
            {
                if (particle.IsAlive())
                {
                    allStopped = false;
                    break;
                }
            }

            if (allStopped)
            {
                m_IsPlaying = false;
                enabled = false;

                OnParticleFinished?.Invoke();
            }
        }
    }
}
