using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CuteGothicCatcher.UI
{
    public class TimerGameOverPanel : Panel
    {
        [Header("Objects")]
        [SerializeField] private GameObject m_ScoreTexts;
        [SerializeField] private GameObject m_HeartsTexts;
        [SerializeField] private List<Button> m_Buttons;

        [Header("Texts")]
        [SerializeField] private TMP_Text m_GameOverText;
        [SerializeField] private TMP_Text m_ScoreValue;
        [SerializeField] private TMP_Text m_HeartsValue;

        [Header("Anim Times")]
        [SerializeField] private float m_ImageFadeTime;
        [SerializeField] private float m_ButtonsOpenTime;
        [SerializeField] private float m_ButtonsCloseTime;

        private float m_StartAlfa;
        
        private Image m_Image;
        private RectTransform m_RectTransform;
        private List<Vector2> m_StartButtonsPos;

        #region Init Methods
        public override void Init()
        {
            base.Init();

            InitImage();
            InitTexts();
            InitButtons();
        }
        private void InitImage()
        {
            m_Image = GetComponent<Image>();
            m_StartAlfa = m_Image.color.a;

            m_Image.DOFade(0, 0);
        }
        private void InitTexts()
        {
            m_GameOverText.transform.localScale = Vector3.zero;
            m_ScoreTexts.transform.localScale = Vector3.zero;
            m_HeartsTexts.transform.localScale = Vector3.zero;
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
        #endregion

        public void SetScore(int score)
        {
            m_ScoreValue.text = score.ToString();
        }
        public void SetHearts(int hearts)
        {
            m_HeartsValue.text = hearts.ToString();
        }

        #region Anim Methods
        protected override void OpenAnim(UnityAction onEndAction = null)
        {
            gameObject.SetActive(true);

            Sequence openSeq = DOTween.Sequence();

            openSeq.Append(m_Image.DOFade(m_StartAlfa, m_ImageFadeTime));
            openSeq.Join(m_GameOverText.transform.DOScale(1, m_ButtonsOpenTime));
            openSeq.Join(m_ScoreTexts.transform.DOScale(1, m_ButtonsOpenTime));
            openSeq.Join(m_HeartsTexts.transform.DOScale(1, m_ButtonsOpenTime));

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
            closeSeq.Join(m_GameOverText.transform.DOScale(0, m_ButtonsOpenTime));
            closeSeq.Join(m_ScoreTexts.transform.DOScale(0, m_ButtonsOpenTime));
            closeSeq.Join(m_HeartsTexts.transform.DOScale(0, m_ButtonsOpenTime));

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
        #endregion
    }
}
