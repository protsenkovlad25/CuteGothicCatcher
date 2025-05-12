using CuteGothicCatcher.Core;
using CuteGothicCatcher.Core.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CuteGothicCatcher.UI
{
    public class QuestSlot : MonoBehaviour, IIniting
    {
        public System.Action OnClickTake;

        [Header("UI Elements")]
        [SerializeField] private Slider m_Slider;
        [SerializeField] private Button m_TakeButton;
        [SerializeField] private GameObject m_FillArea;
        [SerializeField] private GameObject m_RewardPanel;

        [Header("Texts")]
        [SerializeField] private TMP_Text m_TitleText;
        [SerializeField] private TMP_Text m_DescriptionText;
        [SerializeField] private TMP_Text m_ProgressText;
        [SerializeField] private TMP_Text m_ButtonText;
        [SerializeField] private TMP_Text m_RewardValueText;

        private Quest m_Quest;

        public void Init()
        {
            m_FillArea.SetActive(false);
        }

        public void SetQuest(Quest quest)
        {
            m_Quest = quest;

            quest.OnChangedProgress += UpdateProgress;

            SetData();
            UpdateProgress();
        }

        private void SetData()
        {
            m_TitleText.text = m_Quest.Data.Title;
            m_DescriptionText.text = m_Quest.Data.Description;
            m_ButtonText.text = m_Quest.Data.Reward.Value.ToString();
            m_RewardValueText.text = m_Quest.Data.Reward.Value.ToString();

            m_RewardPanel.SetActive(m_Quest.Data.Type == QuestType.Local);
            m_TakeButton.gameObject.SetActive(m_Quest.Data.Type == QuestType.Global && !m_Quest.Data.IsReceived);


            m_Slider.maxValue = m_Quest.Data.Progress.Required;
        }

        public void UpdateProgress()
        {
            m_ProgressText.text = $"{m_Quest.Data.Progress.Current} / {m_Quest.Data.Progress.Required}";
            m_Slider.value = m_Quest.Data.Progress.Current;

            m_FillArea.SetActive(m_Quest.Data.Progress.Current > 0);

            SetButtonState();
            SetSliderState();
        }

        private void SetButtonState()
        {
            if (m_Quest.Data.Type == QuestType.Local || m_Quest.Data.IsReceived)
            {
                m_TakeButton.gameObject.SetActive(false);
                return;
            }

            if (m_Quest.Data.IsComplete)
            {
                m_TakeButton.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                m_TakeButton.interactable = true;
            }
            else m_TakeButton.interactable = false;
        }
        private void SetSliderState()
        {
            if (m_Quest.Data.Type == QuestType.Global)
                m_Slider.gameObject.SetActive(!m_Quest.Data.IsComplete);
        }

        public void ClickTake()
        {
            OnClickTake?.Invoke();
            m_TakeButton.gameObject.SetActive(false);
        }
    }
}
