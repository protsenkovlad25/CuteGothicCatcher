using UnityEngine;

namespace CuteGothicCatcher.Core.Interfaces
{
    public interface ISpawn : IIniting
    {
        void Spawn(Transform transform);
    }
}
