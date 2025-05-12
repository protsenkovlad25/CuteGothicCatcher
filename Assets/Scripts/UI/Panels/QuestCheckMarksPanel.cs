using CuteGothicCatcher.Core;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CuteGothicCatcher.UI
{
    public class QuestCheckMarksPanel : Panel
    {
        [Header("Slots Data")]
        [SerializeField] private QuestCheckMarkSlot m_SlotPrefab;

        private List<QuestCheckMarkSlot> m_Slots;

        public override void Init()
        {
            base.Init();

            m_Slots = new List<QuestCheckMarkSlot>();
        }

        protected override void OpenAnim(UnityAction onEndAction = null)
        {
            gameObject.SetActive(true);

            m_CloseSeq?.Kill();

            Sequence openSeq = DOTween.Sequence();
            m_OpenSeq = openSeq;

            openSeq.Append(m_RectTransform.DOAnchorPos(m_OpenPos, m_OpenTime));

            if (onEndAction != null)
                openSeq.AppendCallback(onEndAction.Invoke);

            openSeq.SetUpdate(true);
        }
        protected override void CloseAnim(UnityAction onEndAction = null)
        {
            m_OpenSeq?.Kill();

            Sequence closeSeq = DOTween.Sequence();
            m_CloseSeq = closeSeq;

            closeSeq.Append(m_RectTransform.DOAnchorPos(m_ClosePos, m_CloseTime));
            closeSeq.AppendCallback(() => { gameObject.SetActive(false); });

            if (onEndAction != null)
                closeSeq.AppendCallback(onEndAction.Invoke);

            closeSeq.SetUpdate(true);
        }

        public void InitSlots(List<Quest> quests)
        {
            foreach (var quest in quests)
                m_Slots.Add(InstantiateSlot(quest));
        }
        public void DestroySlots()
        {
            for (int i = m_Slots.Count - 1; i >= 0; i--)
                Destroy(m_Slots[i].gameObject);

            m_Slots.Clear();
        }

        private QuestCheckMarkSlot InstantiateSlot(Quest quest)
        {
            QuestCheckMarkSlot newSlot = Instantiate(m_SlotPrefab, transform);

            quest.OnCompleted += newSlot.Complete;

            newSlot.Init();

            return newSlot;
        }
    }
}
