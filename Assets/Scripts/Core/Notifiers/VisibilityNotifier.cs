using UnityEngine;
using UnityEngine.Events;

namespace CuteGothicCatcher.Core
{
    public class VisibilityNotifier : MonoBehaviour
    {
        public UnityEvent<bool> OnVisabilityChanged = new UnityEvent<bool>();

        private void OnBecameVisible() => OnVisabilityChanged?.Invoke(true);
        private void OnBecameInvisible() => OnVisabilityChanged?.Invoke(false);
    }
}
