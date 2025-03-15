using CuteGothicCatcher.Core.Interfaces;
using CuteGothicCatcher.Entities;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace CuteGothicCatcher.UI
{
    public class PlacedItemsPanel : MonoBehaviour, IIniting
    {
        public System.Action<PlacedItemSlot> OnSelectSlot;

        [SerializeField] private HorizontalLayoutGroup m_HorLayout;
        [SerializeField] private PlacedItemSlot m_SlotPrefab;

        private List<PlacedItemSlot> m_Slots;
        private PlacedItemSlot m_SelectedSlot;

        public void Init()
        {
            InitSlots();
        }

        private void InitSlots()
        {
            m_Slots = new List<PlacedItemSlot>();

            List<EntityType> types = new List<EntityType>();
            types.AddRange(System.Enum.GetValues(typeof(EntityType)));

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
            m_SelectedSlot.SetSelectState(false);
            m_SelectedSlot = null;
        }
        public void RechargeSlot()
        {
            m_SelectedSlot.Recharge();
            DeselectSlot();
        }

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
