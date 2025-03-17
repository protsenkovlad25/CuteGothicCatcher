using CuteGothicCatcher.Core;
using CuteGothicCatcher.Core.Controllers;
using CuteGothicCatcher.Core.Interfaces;
using CuteGothicCatcher.Entities;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CuteGothicCatcher.UI
{
    public class PlacedItemSlot : MonoBehaviour, IIniting
    {
        public Action<PlacedItemSlot> OnClicked;

        [Header("Images")]
        [SerializeField] private Image m_Background;
        [SerializeField] private Image m_ItemImage;
        [SerializeField] private Image m_SelectIcon;
        [SerializeField] private Image m_RechargeImage;
        [Header("Texts")]
        [SerializeField] private TMP_Text m_AmountText;
        [SerializeField] private TMP_Text m_PriceText;

        private float m_RechargeTime;
        private int m_Price;

        private EntityType m_ItemType;
        private Timer m_RechargeTimer;
        private Button m_Button;

        public EntityType ItemType => m_ItemType;
        public bool IsSelected => m_SelectIcon.gameObject.activeSelf;
        public int Price => m_Price;

        public void Init()
        {
            EntityData data = PoolResources.EntitiesConfig.GetEntityData(m_ItemType);
            PlacedEntityData pData = PoolResources.EntitiesConfig.GetPlacedEntityData(m_ItemType);

            m_RechargeTime = pData.RechargeTime;
            m_Button = GetComponent<Button>();

            SetPrice(pData.Price);
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
        private void SetPrice(int price)
        {
            m_Price = price;
            m_PriceText.text = price.ToString();
        }
        public void SetSelectState(bool state)
        {
            m_SelectIcon.gameObject.SetActive(state);
        }
        private void SetInteractable(bool state)
        {
            m_Button.interactable = state;
        }

        public void ClickSlot()
        {
            if (CheckBuy())
                OnClicked?.Invoke(this);
        }

        private bool CheckBuy()
        {
            return m_Price <= PlayerController.PlayerData.Hearts;
        }

        private void StartRechargeTimer()
        {
            m_RechargeTimer = new Timer(m_RechargeTime);
            m_RechargeTimer.OnTimesUp.AddListener(EndRechargeTimer);

            SetInteractable(false);
        }
        private void EndRechargeTimer()
        {
            m_RechargeTimer = null;
            m_RechargeImage.fillAmount = 0;

            SetInteractable(true);
        }
        private void UpdateTimerAndRechargeImage()
        {
            if (m_RechargeTimer != null)
            {
                m_RechargeImage.fillAmount = m_RechargeTimer.CurrentTime / m_RechargeTime;
                m_RechargeTimer.Update();
            }
        }
        public void Recharge()
        {
            StartRechargeTimer();
        }

        private void Update()
        {
            UpdateTimerAndRechargeImage();
        }
    }
}
