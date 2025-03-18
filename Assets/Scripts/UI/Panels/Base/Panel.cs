using CuteGothicCatcher.Core.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace CuteGothicCatcher.UI
{
    public class Panel : MonoBehaviour, IIniting
    {
        public virtual void Init()
        {
        }

        public virtual void Open(UnityAction onEndAction = null)
        {
            OpenAnim(onEndAction);
        }
        public virtual void Close(UnityAction onEndAction = null)
        {
            CloseAnim(onEndAction);
        }

        #region Animations
        protected virtual void OpenAnim(UnityAction onEndAction = null)
        {
            gameObject.SetActive(true);

            onEndAction?.Invoke();
        }
        protected virtual void CloseAnim(UnityAction onEndAction = null)
        {
            gameObject.SetActive(false);

            onEndAction?.Invoke();
        }
        #endregion
    }
}
