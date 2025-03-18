using CuteGothicCatcher.Entities;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CuteGothicCatcher.UI
{
    public class PlacedItemsPanel : Panel
    {
        public System.Action<PlacedItemSlot> OnSelectSlot;

        [Header("Objects")]
        [SerializeField] private HorizontalLayoutGroup m_HorLayout;
        [SerializeField] private PlacedItemSlot m_SlotPrefab;
        [Header("Anim Values")]
        [SerializeField] private float m_OpenTime;
        [SerializeField] private float m_CloseTime;

        private List<PlacedItemSlot> m_Slots;
        private PlacedItemSlot m_SelectedSlot;

        private Vector2 m_StartPos;
        private RectTransform m_RectTransform;

        public override void Init()
        {
            base.Init();

            InitSlots();

            m_RectTransform = GetComponent<RectTransform>();
            m_StartPos = m_RectTransform.anchoredPosition;

            m_RectTransform.anchoredPosition = new Vector2(m_RectTransform.anchoredPosition.x, -m_RectTransform.sizeDelta.y);
        }

        #region Slots Methods
        private void InitSlots()
        {
            m_Slots = new List<PlacedItemSlot>();

            PlacedItemSlot slot;

            slot = InstantiateSlot(EntityType.Web);
            slot.OnClicked = ClickSlot;

            m_Slots.Add(slot);

            slot = InstantiateSlot(EntityType.Kitty);
            slot.OnClicked = ClickSlot;

            m_Slots.Add(slot);
        }

        private PlacedItemSlot InstantiateSlot(EntityType itemType)
        {
            PlacedItemSlot slot = Instantiate(m_SlotPrefab, m_HorLayout.transform);

            slot.SetItemType(itemType);
            slot.Init();

            return slot;
        }

        private void ClickSlot(PlacedItemSlot slot)
        {
            SelectSlot(slot);
        }
        public void SelectSlot(PlacedItemSlot slot)
        {
            bool isSelected = !slot.IsSelected;

            slot.SetSelectState(isSelected);

            m_SelectedSlot = isSelected ? slot : null;

            if (isSelected)
            {
                foreach (var s in m_Slots)
                    if (s != slot)
                        s.SetSelectState(!isSelected);
            }

            OnSelectSlot?.Invoke(m_SelectedSlot);
        }
        public void DeselectSlot()
        {
            m_SelectedSlot?.SetSelectState(false);
            m_SelectedSlot = null;
        }
        public void RechargeSlot()
        {
            m_SelectedSlot.Recharge();
            DeselectSlot();
        }
        public void DisactiveSlots()
        {
            foreach (var slot in m_Slots)
                slot.Disactive();
        }
        #endregion

        #region Anim Methods
        protected override void OpenAnim(UnityAction onEndAction = null)
        {
            gameObject.SetActive(true);

            Sequence openSeq = DOTween.Sequence();

            openSeq.Append(m_RectTransform.DOAnchorPosY(m_StartPos.y, m_OpenTime));

            if (onEndAction != null)
                openSeq.AppendCallback(onEndAction.Invoke);

            openSeq.SetUpdate(true);
        }
        protected override void CloseAnim(UnityAction onEndAction = null)
        {
            Sequence closeSeq = DOTween.Sequence();

            closeSeq.Append(m_RectTransform.DOAnchorPosY(-m_RectTransform.sizeDelta.y, m_CloseTime));
            closeSeq.AppendCallback(() => { gameObject.SetActive(false); });

            if (onEndAction != null)
                closeSeq.AppendCallback(onEndAction.Invoke);

            closeSeq.SetUpdate(true);
        }
        #endregion

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (m_Slots[0].GetComponent<Button>().interactable)
                    m_Slots[0].GetComponent<Button>().onClick.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (m_Slots[1].GetComponent<Button>().interactable)
                    m_Slots[1].GetComponent<Button>().onClick.Invoke();
            }
        }
    }
}
