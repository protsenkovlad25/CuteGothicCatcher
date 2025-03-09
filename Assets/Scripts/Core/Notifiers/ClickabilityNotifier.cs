using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace CuteGothicCatcher.Core
{
    public class ClickabilityNotifier : MonoBehaviour
    {
        public UnityEvent OnClick = new UnityEvent();

        private void OnMouseDown()
        {
            Debug.Log("CL");
            OnClick?.Invoke();
        }
    }
}
