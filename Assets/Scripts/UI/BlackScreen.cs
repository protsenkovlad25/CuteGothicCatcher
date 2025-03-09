using CuteGothicCatcher.Core.Interfaces;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CuteGothicCatcher.UI
{
    public class BlackScreen : MonoBehaviour, IIniting
    {
        private Image m_Image;

        public void Init()
        {
            m_Image = GetComponent<Image>();
            m_Image.color = new Color(m_Image.color.r, m_Image.color.g, m_Image.color.b, 1f);
        }

        public void Activate(UnityAction onEndAction = null, float duration = 0.5f)
        {
            Sequence s = DOTween.Sequence();

            s.Append(m_Image.DOFade(1f, duration));

            if (onEndAction != null)
                s.AppendCallback(onEndAction.Invoke);
        }

        public void Disactivate(UnityAction onEndAction = null, float duration = 0.5f)
        {
            Sequence s = DOTween.Sequence();

            s.Append(m_Image.DOFade(0f, duration));

            if (onEndAction != null)
                s.AppendCallback(onEndAction.Invoke);
        }
    }
}
