using CuteGothicCatcher.Core;
using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CuteGothicCatcher.UI
{
    public class GlobalQuestsPanel : Panel
    {
        public System.Action<Quest> OnGiveQuestReward;

        [Header("Slots Data")]
        [SerializeField] private QuestSlot m_SlotPrefab;
        [SerializeField] private Transform m_SlotsParent;

        [Header("UI Elements")]
        [SerializeField] private TMP_Text m_TopText;
        [SerializeField] private Button m_BackButton;
        [SerializeField] private ScrollRect m_ScrollRect;

        private List<QuestSlot> m_Slots;

        public override void Init()
        {
            base.Init();

            m_Slots = new List<QuestSlot>();
        }

        #region Anim Methods
        protected override void OpenAnim(UnityAction onEndAction = null)
        {
            gameObject.SetActive(true);

            m_CloseSeq?.Kill();

            Sequence openSeq = DOTween.Sequence();
            m_OpenSeq = openSeq;

            openSeq.Append(m_ScrollRect.transform.DOScale(1, m_OpenTime));
            openSeq.Join(m_TopText.transform.DOScale(1, m_OpenTime));
            openSeq.Join(m_BackButton.transform.DOScale(1, m_OpenTime));

            if (onEndAction != null)
                openSeq.AppendCallback(onEndAction.Invoke);

            openSeq.SetUpdate(true);
        }
        protected override void CloseAnim(UnityAction onEndAction = null)
        {
            m_OpenSeq?.Kill();

            Sequence closeSeq = DOTween.Sequence();
            m_CloseSeq = closeSeq;

            closeSeq.Append(m_ScrollRect.transform.DOScale(0, m_CloseTime));
            closeSeq.Join(m_TopText.transform.DOScale(0, m_CloseTime));
            closeSeq.Join(m_BackButton.transform.DOScale(0, m_CloseTime));

            if (onEndAction != null)
                closeSeq.AppendCallback(onEndAction.Invoke);

            closeSeq.AppendCallback(() => { gameObject.SetActive(false); });

            closeSeq.SetUpdate(true);
        }
        #endregion

        public void InitSlots(List<Quest> quests)
        {
            foreach (var quest in quests)
                m_Slots.Add(InstantiateSlot(quest));
        }

        private QuestSlot InstantiateSlot(Quest quest)
        {
            QuestSlot newSlot = Instantiate(m_SlotPrefab, m_SlotsParent);

            newSlot.OnClickTake = () => { OnGiveQuestReward(quest); };
            newSlot.SetQuest(quest);
            newSlot.Init();

            return newSlot;
        }
    }
}
