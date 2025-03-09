using CuteGothicCatcher.Core;
using CuteGothicCatcher.Core.Interfaces;
using UnityEngine;

namespace CuteGothicCatcher.Entities.Components
{
    public class CollectClickability : MonoBehaviour, IClickable
    {
        [SerializeField] private float m_CollectPoints;
        [SerializeField] private Particle m_CollectParticle; 

        public void Init(BaseEntity self)
        {
        }

        public void Click(BaseEntity self)
        {
            self.TakeDamage(10);
        }

        public void DisableClickability()
        {
        }
    }
}
