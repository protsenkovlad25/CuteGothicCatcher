using CuteGothicCatcher.Core.Controllers;
using CuteGothicCatcher.Entities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CuteGothicCatcher.UI
{
    public class ItemSlot : MonoBehaviour
    {
        [SerializeField] private Image m_Image;
        [SerializeField] private TMP_Text m_CountText;

        private EntityType m_ItemType;

        public EntityType ItemType => m_ItemType;

        public void Init()
        {
            UpdateCount();
        }

        public void SetImage(Sprite sprite)
        {
            m_Image.sprite = sprite;
        }

        public void SetItemType(EntityType type)
        {
            m_ItemType = type;
        }

        public void UpdateCount()
        {
            m_CountText.text = PlayerController.PlayerData.Items[m_ItemType].ToString();
        }
    }
}
