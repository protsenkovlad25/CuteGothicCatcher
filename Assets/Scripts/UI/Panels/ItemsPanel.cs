using CuteGothicCatcher.Entities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CuteGothicCatcher.Core;

namespace CuteGothicCatcher.UI
{
    public class ItemsPanel : Panel
    {
        [SerializeField] private VerticalLayoutGroup m_Layout;
        [SerializeField] private ItemSlot m_SlotPrefab;

        private Dictionary<EntityType, ItemSlot> m_Slots;

        private void Start()
        {
            Init();
        }

        public override void Init()
        {
            InitSlots();

            EventManager.OnChangedItemCount.AddListener(UpdateSlot);
        }

        private void InitSlots()
        {
            m_Slots = new Dictionary<EntityType, ItemSlot>();

            InitSlot(EntityType.Heart);
        }
        private ItemSlot InitSlot(EntityType type)
        {
            ItemSlot slot = InstantiateSlot();
            EntityData data = PoolResources.EntitiesConfig.GetEntityData(type);
            
            slot.SetItemType(type);
            slot.SetImage(data.Sprite);
            slot.Init();

            m_Slots.Add(type, slot);

            return slot;
        }
        private void UpdateSlot(EntityType type, int oldCount, int newCount)
        {
            /*if (m_Slots.ContainsKey(type) || newCount > 0)
            {
                ItemSlot slot = m_Slots.ContainsKey(type) ?
                                m_Slots[type] :
                                InitSlot(type);

                slot.UpdateCount();
            }*/

            if (m_Slots.ContainsKey(type))
            {
                m_Slots[type].UpdateCount();
            }
        }

        private ItemSlot InstantiateSlot()
        {
            ItemSlot slot = Instantiate(m_SlotPrefab, transform);

            return slot;
        }
    }
}
