using CuteGothicCatcher.Core;
using CuteGothicCatcher.Core.Interfaces;
using CuteGothicCatcher.Entities;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CuteGothicCatcher.UI
{
    public class ItemSlot : MonoBehaviour, IIniting
    {
        public Action<ItemSlot> OnClicked;

        [Header("Images")]
        [SerializeField] private Image m_Background;
        [SerializeField] private Image m_ItemImage;
        [SerializeField] private Image m_SelectIcon;
        [Header("Texts")]
        [SerializeField] private TMP_Text m_AmountText;

        private EntityType m_ItemType;

        public EntityType ItemType => m_ItemType;
        public bool IsSelected => m_SelectIcon.gameObject.activeSelf;

        public void Init()
        {
            EntityData data = PoolResources.EntitiesConfig.GetData(m_ItemType);

            SetItemImage(data.Sprite);
            SetSelectState(false);
        }

        public void SetItemType(EntityType itemType)
        {
            m_ItemType = itemType;
        }

        private void SetItemImage(Sprite sprite)
        {
            m_ItemImage.sprite = sprite;
        }

        public void SetSelectState(bool state)
        {
            m_SelectIcon.gameObject.SetActive(state);
        }

        public void ClickSlot()
        {
            OnClicked?.Invoke(this);
        }
    }
}
