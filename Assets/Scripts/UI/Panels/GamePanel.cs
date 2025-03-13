using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CuteGothicCatcher.UI
{
    public class GamePanel : Panel
    {
        [Header("Objects")]
        [SerializeField] private Button m_PauseButton;
        [SerializeField] private ItemsPanel m_ItemsPanel;

        [Header("Anim Times")]
        [SerializeField] private float m_OpenTime;
        [SerializeField] private float m_CloseTime;

        private Vector2 m_StartButtonPos;

        public override void Init()
        {
            base.Init();

            m_ItemsPanel.Init();

            RectTransform rectTransform = m_PauseButton.GetComponent<RectTransform>();
            m_StartButtonPos = rectTransform.anchoredPosition;

            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.sizeDelta.y);
        }

        protected override void OpenAnim(UnityAction onEndAction = null)
        {
            gameObject.SetActive(true);

            RectTransform rectTransform = m_PauseButton.GetComponent<RectTransform>();

            Sequence openSeq = DOTween.Sequence();

            openSeq.Append(rectTransform.DOAnchorPosY(m_StartButtonPos.y, m_OpenTime));

            if (onEndAction != null)
                openSeq.AppendCallback(onEndAction.Invoke);

            openSeq.SetUpdate(true);
        }

        protected override void CloseAnim(UnityAction onEndAction = null)
        {
            RectTransform rectTransform = m_PauseButton.GetComponent<RectTransform>();

            Sequence closeSeq = DOTween.Sequence();

            closeSeq.Append(rectTransform.DOAnchorPosY(rectTransform.sizeDelta.y, m_CloseTime));
            closeSeq.AppendCallback(() => { gameObject.SetActive(false); });

            if (onEndAction != null)
                closeSeq.AppendCallback(onEndAction.Invoke);

            closeSeq.SetUpdate(true);
        }
    }
}
