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
        [SerializeField] private GameTimer m_GameTimer;
        [SerializeField] private PlacedItemsPanel m_PlacedItemsPanel;

        [Header("Anim Times")]
        [SerializeField] private float m_OpenTime;
        [SerializeField] private float m_CloseTime;

        private Vector2 m_StartButtonPos;

        public override void Init()
        {
            base.Init();

            RectTransform rectTransform = m_PauseButton.GetComponent<RectTransform>();
            m_StartButtonPos = rectTransform.anchoredPosition;

            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.sizeDelta.y);
        }

        #region Anim Methods
        protected override void OpenAnim(UnityAction onEndAction = null)
        {
            gameObject.SetActive(true);

            RectTransform rectTransform = m_PauseButton.GetComponent<RectTransform>();

            Sequence openSeq = DOTween.Sequence();

            openSeq.Append(rectTransform.DOAnchorPosY(m_StartButtonPos.y, m_OpenTime));
            openSeq.JoinCallback(() => { m_GameTimer.Open(); });
            openSeq.JoinCallback(() => { m_PlacedItemsPanel.Open(); });

            if (onEndAction != null)
                openSeq.AppendCallback(onEndAction.Invoke);

            openSeq.SetUpdate(true);
        }
        protected override void CloseAnim(UnityAction onEndAction = null)
        {
            RectTransform rectTransform = m_PauseButton.GetComponent<RectTransform>();

            Sequence closeSeq = DOTween.Sequence();

            closeSeq.Append(rectTransform.DOAnchorPosY(rectTransform.sizeDelta.y, m_CloseTime));
            closeSeq.JoinCallback(() => { m_GameTimer.Close(); });
            closeSeq.JoinCallback(() => { m_PlacedItemsPanel.Close(); });
            closeSeq.AppendCallback(() => { gameObject.SetActive(false); });

            if (onEndAction != null)
                closeSeq.AppendCallback(onEndAction.Invoke);

            closeSeq.SetUpdate(true);
        }
        #endregion
    }
}
