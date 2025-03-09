using CuteGothicCatcher.Core.Interfaces;
using UnityEngine;

namespace CuteGothicCatcher.Objects
{
    public class SlowEffectSprite : MonoBehaviour, IPoolable
    {
        public event IPoolable.Disabled OnDisabled;

        public void OnActivate()
        {
        }

        public void OnDeactivate()
        {
        }

        public void Disable()
        {
            OnDisabled?.Invoke(this);
        }
    }
}
