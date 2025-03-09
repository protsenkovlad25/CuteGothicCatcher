using CuteGothicCatcher.Core.Interfaces;
using UnityEngine;

namespace CuteGothicCatcher.Core
{
    public abstract class Controller : MonoBehaviour, IIniting
    {
        public abstract void Init();
    }
}
