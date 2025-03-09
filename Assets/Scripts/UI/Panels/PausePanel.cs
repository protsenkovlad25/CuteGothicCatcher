using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CuteGothicCatcher.UI
{
    public class PausePanel : Panel
    {
        [Header("Objects")]
        [SerializeField] private List<Button> m_Buttons;
        [Header("Anim Times")]
        [SerializeField] private float m_ImageFadeTime;
        [SerializeField] private float m_ButtonsOpenTime;
        [SerializeField] private float m_ButtonsCloseTime;

        private List<Vector2> m_StartButtonsPos;
        private Image m_Image;
        private float m_StartAlfa;

        public override void Init()
        {
            base.Init();

            InitImage();
            InitButtons();
        }

        private void InitImage()
        {
            m_Image = GetComponent<Image>();
            m_StartAlfa = m_Image.color.a;

            m_Image.DOFade(0, 0);
        }
        private void InitButtons()
        {
            m_StartButtonsPos = new List<Vector2>();

            RectTransform rectTransform;
            foreach (var button in m_Buttons)
            {
                rectTransform = button.GetComponent<RectTransform>();
                m_StartButtonsPos.Add(rectTransform.anchoredPosition);

                button.transform.localScale = Vector3.zero;
            }
        }

        protected override void OpenAnim(UnityAction onEndAction = null)
        {
            gameObject.SetActive(true);

            Sequence openSeq = DOTween.Sequence();

            openSeq.Append(m_Image.DOFade(m_StartAlfa, m_ImageFadeTime));

            RectTransform rectTransform;
            for (int i = 0; i < m_Buttons.Count; i++)
            {
                rectTransform = m_Buttons[i].GetComponent<RectTransform>();

                openSeq.Join(rectTransform.DOScale(1, m_ButtonsOpenTime));
            }

            if (onEndAction != null)
                openSeq.AppendCallback(onEndAction.Invoke);

            openSeq.SetUpdate(true);
        }

        protected override void CloseAnim(UnityAction onEndAction = null)
        {
            Sequence closeSeq = DOTween.Sequence();

            closeSeq.Append(m_Image.DOFade(0, m_ImageFadeTime));

            RectTransform rectTransform;
            for (int i = 0; i < m_Buttons.Count; i++)
            {
                rectTransform = m_Buttons[i].GetComponent<RectTransform>();

                closeSeq.Join(rectTransform.DOScale(0, m_ButtonsCloseTime));

                if (i == m_Buttons.Count - 1)
                    closeSeq.AppendInterval(m_ButtonsCloseTime);
            }

            if (onEndAction != null)
                closeSeq.AppendCallback(onEndAction.Invoke);

            closeSeq.AppendCallback(() => { gameObject.SetActive(false); });

            closeSeq.SetUpdate(true);
        }
    }
}
