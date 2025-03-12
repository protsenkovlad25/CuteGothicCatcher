using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CuteGothicCatcher.UI
{
    public class MenuPanel : Panel
    {
        [Header("Objects")]
        [SerializeField] private List<Button> m_Buttons;
        [SerializeField] private VerticalLayoutGroup m_Layout;

        [Header("Anim Values")]
        [SerializeField] private float m_ButtonsOpenTime;
        [SerializeField] private float m_ButtonsCloseTime;
        [SerializeField] private float m_Padding;

        private List<Vector2> m_StartButtonsPos;

        public override void Init()
        {
            base.Init();

            InitButtons();
        }

        private void InitButtons()
        {
            m_StartButtonsPos = new List<Vector2>();

            RectTransform rectTransform;
            foreach (var button in m_Buttons)
            {
                rectTransform = button.GetComponent<RectTransform>();
                m_StartButtonsPos.Add(rectTransform.anchoredPosition);

                rectTransform.anchoredPosition = new Vector2(-(rectTransform.sizeDelta.x + m_Padding), rectTransform.anchoredPosition.y);
            }
        }

        protected override void OpenAnim(UnityAction onEndAction = null)
        {
            gameObject.SetActive(true);

            Sequence openSeq = DOTween.Sequence();

            RectTransform rectTransform;
            for (int i = 0; i < m_Buttons.Count; i++)
            {
                rectTransform = m_Buttons[i].GetComponent<RectTransform>();

                openSeq.Append(rectTransform.DOAnchorPosX(m_StartButtonsPos[i].x, m_ButtonsOpenTime));
                //openSeq.AppendInterval(i < m_Buttons.Count - 1 ? m_ButtonsOpenTime / 2 : m_ButtonsOpenTime);
            }

            if (onEndAction != null)
                openSeq.AppendCallback(onEndAction.Invoke);

            openSeq.SetUpdate(true);
        }

        protected override void CloseAnim(UnityAction onEndAction = null)
        {
            Sequence closeSeq = DOTween.Sequence();

            RectTransform rectTransform;
            for (int i = 0; i < m_Buttons.Count; i++)
            {
                rectTransform = m_Buttons[i].GetComponent<RectTransform>();

                closeSeq.Append(rectTransform.DOAnchorPosX(-(rectTransform.sizeDelta.x + m_Padding), m_ButtonsCloseTime));

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
