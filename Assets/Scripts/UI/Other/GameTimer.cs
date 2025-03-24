using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CuteGothicCatcher.UI
{
    public class GameTimer : Panel
    {
        [SerializeField] private float m_WaitTime;
        [SerializeField] private float m_RotateTime;
        [SerializeField] private Ease m_Ease;

        [Header("Objects")]
        [SerializeField] private Image m_TimerArea;
        [SerializeField] private Image m_TimerIcon;

        private Sequence m_RotateSequence;

        public override void Init()
        {
            base.Init();

            InitRotateAnim();
            StopRotateAnim();
        }

        public void SetAreaAmount(float amount)
        {
            m_TimerArea.fillAmount = amount;
        }

        #region Anim Methods
        protected override void OpenAnim(UnityAction onEndAction = null)
        {
            gameObject.SetActive(true);

            m_CloseSeq?.Kill();

            SetAreaAmount(1);

            Sequence openSeq = DOTween.Sequence();
            m_OpenSeq = openSeq;

            openSeq.Append(m_RectTransform.DOAnchorPosX(m_OpenPos.x, m_OpenTime));

            if (onEndAction != null)
                openSeq.AppendCallback(onEndAction.Invoke);

            openSeq.SetUpdate(true);
        }
        protected override void CloseAnim(UnityAction onEndAction = null)
        {
            m_OpenSeq?.Kill();

            Sequence closeSeq = DOTween.Sequence();
            m_CloseSeq = closeSeq;

            closeSeq.Append(m_RectTransform.DOAnchorPosX(m_ClosePos.x, m_CloseTime));
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
