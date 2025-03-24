using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CuteGothicCatcher.UI
{
    public class GameModesPanel : Panel
    {
        [SerializeField] private float m_ButtonsOpenTime;
        [SerializeField] private float m_ButtonsCloseTime;
        [SerializeField] private float m_Padding;

        [Header("Objects")]
        [SerializeField] private List<Button> m_Buttons;
        [SerializeField] private TMP_Text m_GameModesText;

        private List<Vector2> m_ButtonsStartPos;
        private Vector2 m_TextStartPos;

        private Sequence m_OpenSequence;
        private Sequence m_CloseSequence;

        protected override void InitStartPosition()
        {
            base.InitStartPosition();

            RectTransform textRectTransform = m_GameModesText.GetComponent<RectTransform>();
            m_TextStartPos = textRectTransform.anchoredPosition;
            textRectTransform.anchoredPosition = new Vector2(-textRectTransform.sizeDelta.x, textRectTransform.anchoredPosition.y);

            m_ButtonsStartPos = new List<Vector2>();

            RectTransform rectTransform;
            foreach (var button in m_Buttons)
            {
                rectTransform = button.GetComponent<RectTransform>();
                m_ButtonsStartPos.Add(rectTransform.anchoredPosition);

                rectTransform.anchoredPosition = new Vector2(-(rectTransform.sizeDelta.x + m_Padding), rectTransform.anchoredPosition.y);
            }
        }

        protected override void OpenAnim(UnityAction onEndAction = null)
        {
            gameObject.SetActive(true);

            m_CloseSequence?.Kill();

            Sequence openSeq = DOTween.Sequence();
            m_OpenSequence = openSeq;

            RectTransform rectTransform;
            for (int i = 0; i < m_Buttons.Count; i++)
            {
                rectTransform = m_Buttons[i].GetComponent<RectTransform>();

                openSeq.Append(rectTransform.DOAnchorPosX(m_ButtonsStartPos[i].x, m_ButtonsOpenTime));
                //openSeq.AppendInterval(i < m_Buttons.Count - 1 ? m_ButtonsOpenTime / 2 : m_ButtonsOpenTime);

                if (i == 0)
                    openSeq.Join(m_GameModesText.GetComponent<RectTransform>().DOAnchorPosX(m_TextStartPos.x, m_ButtonsOpenTime));
            }

            if (onEndAction != null)
                openSeq.AppendCallback(onEndAction.Invoke);

            openSeq.SetUpdate(true);
        }
        protected override void CloseAnim(UnityAction onEndAction = null)
        {
            m_OpenSequence?.Kill();

            Sequence closeSeq = DOTween.Sequence();
            m_CloseSequence = closeSeq;

            RectTransform rectTransform;
            for (int i = 0; i < m_Buttons.Count; i++)
            {
                rectTransform = m_Buttons[i].GetComponent<RectTransform>();

                closeSeq.Append(rectTransform.DOAnchorPosX(-(rectTransform.sizeDelta.x + m_Padding), m_ButtonsCloseTime));

                if (i == 0)
                    closeSeq.Join(m_GameModesText.GetComponent<RectTransform>().DOAnchorPosX(-m_GameModesText.GetComponent<RectTransform>().sizeDelta.x, m_ButtonsOpenTime));

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
