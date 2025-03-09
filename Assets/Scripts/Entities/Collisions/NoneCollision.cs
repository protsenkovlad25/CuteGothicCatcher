using CuteGothicCatcher.Core.Interfaces;
using UnityEngine;

namespace CuteGothicCatcher.Entities.Components
{
    public class NoneCollision : MonoBehaviour, IColliding
    {
        public void Init(BaseEntity self)
        {
        }

        public void Collision(BaseEntity self, Collision2D collision)
        {
        }

        public void TriggerEnter(BaseEntity self, Collider2D collider)
        {
        }

        public void TriggerExit(BaseEntity self, Collider2D collider)
        {
        }

        public void DisableCollision()
        {
        }
    }
}
