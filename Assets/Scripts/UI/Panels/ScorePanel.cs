using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace CuteGothicCatcher.UI
{
    public class ScorePanel : Panel
    {
        [Header("Objects")]
        [SerializeField] private TMP_Text m_ValueText;

        [Header("Anima Values")]
        [SerializeField] private float m_OpenTime;
        [SerializeField] private float m_CloseTime;

        private Vector2 m_StartPos;
        private RectTransform m_RectTransform;

        public override void Init()
        {
            base.Init();

            m_RectTransform = GetComponent<RectTransform>();
            m_StartPos = m_RectTransform.anchoredPosition;

            m_RectTransform.anchoredPosition = new Vector2(-m_RectTransform.sizeDelta.x, m_RectTransform.anchoredPosition.y);
        }

        public void SetScoreValue(int value)
        {
            m_ValueText.text = value.ToString();
        }

        #region Anim Methods
        protected override void OpenAnim(UnityAction onEndAction = null)
        {
            gameObject.SetActive(true);

            Sequence openSeq = DOTween.Sequence();

            openSeq.Append(m_RectTransform.DOAnchorPosX(m_StartPos.x, m_OpenTime));

            if (onEndAction != null)
                openSeq.AppendCallback(onEndAction.Invoke);

            openSeq.SetUpdate(true);
        }
        protected override void CloseAnim(UnityAction onEndAction = null)
        {
            Sequence closeSeq = DOTween.Sequence();

            closeSeq.Append(m_RectTransform.DOAnchorPosX(-m_RectTransform.sizeDelta.x, m_CloseTime));
            closeSeq.AppendCallback(() => { gameObject.SetActive(false); });

            if (onEndAction != null)
                closeSeq.AppendCallback(onEndAction.Invoke);

            closeSeq.SetUpdate(true);
        }
        #endregion
    }
}
