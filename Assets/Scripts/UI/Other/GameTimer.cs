using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CuteGothicCatcher.UI
{
    public class GameTimer : Panel
    {
        [Header("Objects")]
        [SerializeField] private Image m_TimerArea;
        [SerializeField] private Image m_TimerIcon;

        [Header("Anima Values")]
        [SerializeField] private float m_OpenTime;
        [SerializeField] private float m_CloseTime;
        [SerializeField] private float m_WaitTime;
        [SerializeField] private float m_RotateTime;
        [SerializeField] private Ease m_Ease;

        private Vector2 m_StartPos;
        private RectTransform m_RectTransform;
        private Sequence m_RotateSequence;

        public override void Init()
        {
            base.Init();

            InitRotateAnim();
            StopRotateAnim();

            m_RectTransform = GetComponent<RectTransform>();
            m_StartPos = m_RectTransform.anchoredPosition;

            m_RectTransform.anchoredPosition = new Vector2(-m_RectTransform.sizeDelta.x, m_RectTransform.anchoredPosition.y);
        }

        public void SetAreaAmount(float amount)
        {
            m_TimerArea.fillAmount = amount;
        }

        #region Anim Methods
        protected override void OpenAnim(UnityAction onEndAction = null)
        {
            gameObject.SetActive(true);

            SetAreaAmount(1);

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
        private void InitRotateAnim()
        {
            Sequence s = DOTween.Sequence();
            m_RotateSequence = s;

            s.AppendInterval(m_WaitTime);
            s.Append(m_TimerIcon.transform.DOLocalRotate(new Vector3(0, 0, 180), m_RotateTime)).SetEase(m_Ease);
            s.AppendInterval(m_WaitTime);
            s.Append(m_TimerIcon.transform.DOLocalRotate(new Vector3(0, 0, 360), m_RotateTime)).SetEase(m_Ease);
            s.SetLoops(-1);
        }
        public void PlayRoteteAnim()
        {
            m_RotateSequence.Restart();
        }
        public void StopRotateAnim()
        {
            m_RotateSequence.Pause();
            m_TimerIcon.transform.DOLocalRotate(new Vector3(0, 0, 0), .5f);
        }
        #endregion
    }
}
