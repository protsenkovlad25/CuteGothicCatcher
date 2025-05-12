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
        [SerializeField] private ScorePanel m_ScorePanel;
        [SerializeField] private PlacedItemsPanel m_PlacedItemsPanel;
        [SerializeField] private QuestCheckMarksPanel m_QuestCheckMarksPanel;

        private Vector2 m_StartButtonPos;

        protected override void InitStartPosition()
        {
            base.InitStartPosition();

            RectTransform rectTransform = m_PauseButton.GetComponent<RectTransform>();
            m_StartButtonPos = rectTransform.anchoredPosition;

            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.sizeDelta.y);
        }

        #region Anim Methods
        protected override void OpenAnim(UnityAction onEndAction = null)
        {
            gameObject.SetActive(true);

            m_CloseSeq?.Kill();

            RectTransform rectTransform = m_PauseButton.GetComponent<RectTransform>();

            Sequence openSeq = DOTween.Sequence();
            m_OpenSeq = openSeq;

            openSeq.Append(rectTransform.DOAnchorPosY(m_StartButtonPos.y, m_OpenTime));
            openSeq.JoinCallback(() =>
            {
                m_GameTimer.Open();
                m_ScorePanel.Open();
                m_PlacedItemsPanel.Open();
                m_QuestCheckMarksPanel.Open();
            });

            if (onEndAction != null)
                openSeq.AppendCallback(onEndAction.Invoke);

            openSeq.SetUpdate(true);
        }
        protected override void CloseAnim(UnityAction onEndAction = null)
        {
            m_OpenSeq?.Kill();

            RectTransform rectTransform = m_PauseButton.GetComponent<RectTransform>();

            Sequence closeSeq = DOTween.Sequence();
            m_CloseSeq = closeSeq;

            closeSeq.Append(rectTransform.DOAnchorPosY(rectTransform.sizeDelta.y, m_CloseTime));
            closeSeq.JoinCallback(() =>
            {
                m_GameTimer.Close();
                m_ScorePanel.Close();
                m_PlacedItemsPanel.Close();
                m_QuestCheckMarksPanel.Close();
            });
            closeSeq.AppendCallback(() => { gameObject.SetActive(false); });

            if (onEndAction != null)
                closeSeq.AppendCallback(onEndAction.Invoke);

            closeSeq.SetUpdate(true);
        }
        #endregion
    }
}
