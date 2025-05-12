using CuteGothicCatcher.Core;
using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CuteGothicCatcher.UI
{
    public class LocalQuestsPanel : Panel
    {
        [SerializeField] private float m_ImageFadeTime;
        [SerializeField] private float m_OtherCloseOpenTime;

        [Header("Slots Data")]
        [SerializeField] private QuestSlot m_SlotPrefab;
        [SerializeField] private Transform m_SlotsParent;

        [Header("Elements")]
        [SerializeField] private TMP_Text m_TopText;
        [SerializeField] private Button m_BackButton;

        private List<QuestSlot> m_Slots;
        private Image m_Image;

        private float m_StartAlfa;

        #region InitMethods
        public override void Init()
        {
            base.Init();

            m_Slots = new List<QuestSlot>();

            InitImage();
        }
        private void InitImage()
        {
            m_Image = GetComponent<Image>();
            m_StartAlfa = m_Image.color.a;

            m_Image.DOFade(0, 0);
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
            openSeq.Join(m_SlotsParent.DOScale(1, m_OtherCloseOpenTime));
            openSeq.Join(m_TopText.transform.DOScale(1, m_OtherCloseOpenTime));
            openSeq.Join(m_BackButton.transform.DOScale(1, m_OtherCloseOpenTime));

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
            closeSeq.Join(m_SlotsParent.DOScale(0, m_OtherCloseOpenTime));
            closeSeq.Join(m_TopText.transform.DOScale(0, m_OtherCloseOpenTime));
            closeSeq.Join(m_BackButton.transform.DOScale(0, m_OtherCloseOpenTime));

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
        public void DestroySlots()
        {
            for (int i = m_Slots.Count - 1; i >= 0; i--)
                Destroy(m_Slots[i].gameObject);

            m_Slots.Clear();
        }

        private QuestSlot InstantiateSlot(Quest quest)
        {
            QuestSlot newSlot = Instantiate(m_SlotPrefab, m_SlotsParent);

            newSlot.SetQuest(quest);
            newSlot.Init();

            return newSlot;
        }
    }
}
