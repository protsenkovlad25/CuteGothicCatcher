using CuteGothicCatcher.Core;
using CuteGothicCatcher.Entities;
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
        [SerializeField] private float m_ImageFadeTime;
        [Space]
        [SerializeField] private float m_ButtonsOpenTime;
        [SerializeField] private float m_ButtonsOpenIntervalTime;
        [SerializeField] private float m_ButtonsCloseTime;
        [Space]
        [SerializeField] private float m_OtherOpenTime;
        [SerializeField] private float m_OtherCloseTime;

        [Header("Objects")]
        [SerializeField] private GameObject m_ScorePanel;
        [SerializeField] private GameObject m_HeartsPanel;
        [SerializeField] private GameObject m_SpendedHeartsPanel;
        [Space]
        [SerializeField] private GameObject m_CollectedEntitiesPanel;
        [SerializeField] private GameObject m_DestroyedEntitiesPanel;
        [SerializeField] private Transform m_CollectedContainer;
        [SerializeField] private Transform m_DestroyedContainer;
        [SerializeField] private GameObject m_ValueSlotPrefab;
        [Space]
        [SerializeField] private List<Button> m_Buttons;

        [Header("Texts")]
        [SerializeField] private TMP_Text m_GameOverText;
        [SerializeField] private TMP_Text m_ScoreValueText;
        [SerializeField] private TMP_Text m_HeartsValueText;
        [SerializeField] private TMP_Text m_SpendedHeartsValueText;

        private float m_StartAlfa;
        
        private Image m_Image;
        private List<Vector2> m_StartButtonsPos;
        private Dictionary<EntityType, GameObject> m_CollectedValueSlots;
        private Dictionary<EntityType, GameObject> m_DestroyedValueSlots;

        #region Init Methods
        public override void Init()
        {
            base.Init();

            InitImage();
            InitTexts();
            InitButtons();
            InitPanels();
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
            m_ScorePanel.transform.localScale = Vector3.zero;
            m_HeartsPanel.transform.localScale = Vector3.zero;
            m_SpendedHeartsPanel.transform.localScale = Vector3.zero;
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
        private void InitPanels()
        {
            m_CollectedValueSlots = new Dictionary<EntityType, GameObject>();
            m_DestroyedValueSlots = new Dictionary<EntityType, GameObject>();

            foreach (EntityType type in System.Enum.GetValues(typeof(EntityType)))
            {
                if (type != EntityType.None)
                {
                    m_CollectedValueSlots.Add(type, Instantiate(m_ValueSlotPrefab, m_CollectedContainer));
                    m_DestroyedValueSlots.Add(type, Instantiate(m_ValueSlotPrefab, m_DestroyedContainer));
                }
            }

            m_CollectedEntitiesPanel.transform.localScale = Vector3.zero;
            m_DestroyedEntitiesPanel.transform.localScale = Vector3.zero;
        }
        #endregion

        #region Set Methods
        public void SetScore(int score)
        {
            m_ScoreValueText.text = score.ToString();
        }
        public void SetHearts(int hearts)
        {
            m_HeartsValueText.text = hearts.ToString();
        }
        public void SetSpendedHearts(int hearts)
        {
            m_SpendedHeartsValueText.text = hearts.ToString();
        }
        public void SetCollectedEntities(Dictionary<EntityType, int> collectedEntities)
        {
            foreach (var item in collectedEntities)
            {
                m_CollectedValueSlots[item.Key].GetComponentInChildren<TMP_Text>().text = item.Value.ToString();
                m_CollectedValueSlots[item.Key].GetComponentInChildren<Image>().sprite = PoolResources.EntitiesConfig.GetEntityData(item.Key).Sprite;
                m_CollectedValueSlots[item.Key].SetActive(item.Value > 0);
            }
        }
        public void SetDestroyedEntities(Dictionary<EntityType, int> destroyedEntities)
        {
            foreach (var item in destroyedEntities)
            {
                m_DestroyedValueSlots[item.Key].GetComponentInChildren<TMP_Text>().text = item.Value.ToString();
                m_DestroyedValueSlots[item.Key].GetComponentInChildren<Image>().sprite = PoolResources.EntitiesConfig.GetEntityData(item.Key).Sprite;
                m_DestroyedValueSlots[item.Key].SetActive(item.Value > 0);
            }
        }
        #endregion

        #region Anim Methods
        protected override void OpenAnim(UnityAction onEndAction = null)
        {
            gameObject.SetActive(true);

            m_CloseSeq?.Kill();

            Sequence openSeq = DOTween.Sequence();
            m_OpenSeq = openSeq;

            openSeq.Append(m_Image.DOFade(m_StartAlfa, m_ImageFadeTime));
            openSeq.Join(m_GameOverText.transform.DOScale(1, m_OtherOpenTime));
            openSeq.Append(m_ScorePanel.transform.DOScale(1, m_OtherOpenTime));
            openSeq.Append(m_CollectedEntitiesPanel.transform.DOScale(1, m_OtherOpenTime));
            openSeq.Join(m_DestroyedEntitiesPanel.transform.DOScale(1, m_OtherOpenTime));
            openSeq.Append(m_SpendedHeartsPanel.transform.DOScale(1, m_OtherOpenTime));
            openSeq.AppendInterval(m_ButtonsOpenIntervalTime);

            RectTransform rectTransform;
            for (int i = 0; i < m_Buttons.Count; i++)
            {
                rectTransform = m_Buttons[i].GetComponent<RectTransform>();

                if (i == 0)
                    openSeq.Append(rectTransform.DOScale(1, m_ButtonsOpenTime));
                else
                    openSeq.Join(rectTransform.DOScale(1, m_ButtonsOpenTime));
            }

            if (onEndAction != null)
                openSeq.AppendCallback(onEndAction.Invoke);

            openSeq.SetUpdate(true);
        }
        protected override void CloseAnim(UnityAction onEndAction = null)
        {
            m_OpenSeq?.Kill();

            Sequence closeSeq = DOTween.Sequence();
            m_CloseSeq = closeSeq;

            closeSeq.Append(m_Image.DOFade(0, m_ImageFadeTime));
            closeSeq.Join(m_GameOverText.transform.DOScale(0, m_OtherCloseTime));
            closeSeq.Join(m_ScorePanel.transform.DOScale(0, m_OtherCloseTime));
            closeSeq.Join(m_CollectedEntitiesPanel.transform.DOScale(0, m_OtherCloseTime));
            closeSeq.Join(m_DestroyedEntitiesPanel.transform.DOScale(0, m_OtherCloseTime));
            closeSeq.Join(m_SpendedHeartsPanel.transform.DOScale(0, m_OtherCloseTime));

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
