using CuteGothicCatcher.Entities;
using UnityEngine;

namespace CuteGothicCatcher.Core.Interfaces
{
    public interface IColliding
    {
        void Init(BaseEntity self);
        void Collision(BaseEntity self, Collision2D collision);
        void TriggerEnter(BaseEntity self, Collider2D collider);
        void TriggerExit(BaseEntity self, Collider2D collider);
        void DisableCollision();
    }
}
