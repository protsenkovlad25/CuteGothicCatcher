using CuteGothicCatcher.Core.Interfaces;
using CuteGothicCatcher.Entities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CuteGothicCatcher.UI
{
    public class ItemsPanel : MonoBehaviour, IIniting
    {
        [SerializeField] private HorizontalLayoutGroup m_HorLayout;
        [SerializeField] private ItemSlot m_SlotPrefab;

        private List<ItemSlot> m_Slots;
        private ItemSlot m_SelectedSlot;

        public void Init()
        {
            InitSlots();
        }

        private void InitSlots()
        {
            m_Slots = new List<ItemSlot>();

            /*List<EntityType> types = new List<EntityType>();
            types.AddRange(Enum.GetValues(typeof(EntityType)));
            
            ItemSlot slot;
            for (int i = 0; i < 3; i++)
            {
                slot = InstantiateSlot(types[i + 1]);
                slot.OnClicked = ClickSlot;

                m_Slots.Add(slot);
            }*/
        }

        private ItemSlot InstantiateSlot(EntityType itemType)
        {
            ItemSlot slot = Instantiate(m_SlotPrefab, m_HorLayout.transform);

            slot.SetItemType(itemType);
            slot.Init();

            return slot;
        }

        private void ClickSlot(ItemSlot slot)
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
        }
    }
}
