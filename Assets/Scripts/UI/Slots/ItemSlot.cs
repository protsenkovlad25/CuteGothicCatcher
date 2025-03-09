using CuteGothicCatcher.Core;
using CuteGothicCatcher.Core.Interfaces;
using CuteGothicCatcher.Entities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CuteGothicCatcher.UI
{
    public class ItemSlot : MonoBehaviour, IIniting
    {
        [Header("Images")]
        [SerializeField] private Image m_Background;
        [SerializeField] private Image m_ItemImage;
        [SerializeField] private Image m_SelectIcon;
        [Header("Texts")]
        [SerializeField] private TMP_Text m_AmountText;

        private EntityType m_ItemType;

        public EntityType ItemType => m_ItemType;

        public void Init()
        {
            EntityData data = PoolResources.EntitiesConfig.GetData(m_ItemType);

            SetItemImage(data.Sprite);
        }

        public void SetItemType(EntityType itemType)
        {
            m_ItemType = itemType;
        }

        private void SetItemImage(Sprite sprite)
        {
            m_ItemImage.sprite = sprite;
        }
    }
}
