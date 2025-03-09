using CuteGothicCatcher.Core.Interfaces;
using CuteGothicCatcher.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace CuteGothicCatcher.UI
{
    public class ItemsPanel : MonoBehaviour, IIniting
    {
        [SerializeField] private HorizontalLayoutGroup m_HorLayout;
        [SerializeField] private ItemSlot m_SlotPrefab;

        public void Init()
        {
        }

        private ItemSlot InstantiateSlot(EntityType itemType)
        {
            ItemSlot slot = Instantiate(m_SlotPrefab, m_HorLayout.transform);

            slot.SetItemType(itemType);
            slot.Init();

            return slot;
        }
    }
}
