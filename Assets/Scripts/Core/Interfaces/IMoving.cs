using UnityEngine;

namespace CuteGothicCatcher.Core.Interfaces
{
    public interface IMoving
    {
        void Init(Rigidbody2D rigidbody, Transform transform);

        void Move();

        void StartMove();
        void StopMove();

        void DisableMove();
    }
}
