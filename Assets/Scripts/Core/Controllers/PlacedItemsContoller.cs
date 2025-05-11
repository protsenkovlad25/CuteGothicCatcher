using CuteGothicCatcher.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CuteGothicCatcher.Core.Controllers
{
    public class PlacedItemsContoller : Controller
    {
        [SerializeField] private PlacedItemsPanel m_PlacedItemsPanel;
        [SerializeField] private EntitiesController m_EntitiesController;

        private PlacedItemSlot m_SelectedSlot;

        public override void Init()
        {
            m_PlacedItemsPanel.Init();
            m_PlacedItemsPanel.OnSelectSlot = SelectSlot;
        }

        #region Slot Methods
        private void SelectSlot(PlacedItemSlot slot)
        {
            m_SelectedSlot = slot;
        }
        public void DeselectSlot()
        {
            m_SelectedSlot = null;
            m_PlacedItemsPanel.DeselectSlot();
        }
        private void RechargeSlot()
        {
            m_SelectedSlot = null;
            m_PlacedItemsPanel.RechargeSlot();
        }
        public void DisactiveSlots()
        {
            DeselectSlot();
            m_PlacedItemsPanel.DisactiveSlots();
        }
        #endregion

        private void BuyAndSpawnItem(Vector2 position)
        {
            m_EntitiesController.SpawnEntity(m_SelectedSlot.ItemType, position);

            PlayerController.PlayerData.SpendHearts(m_SelectedSlot.Price);

            EventManager.PlacedItem(m_SelectedSlot.ItemType);

            RechargeSlot();
        }

        private void CheckClickOnScreen()
        {
            if (IsPointerOverUIObject() || m_SelectedSlot == null) return;

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                BuyAndSpawnItem(MainCamera.Camera.ScreenToWorldPoint(Input.GetTouch(0).position));
            }

#if UNITY_EDITOR
            if (Input.GetMouseButtonUp(0))
            {
                BuyAndSpawnItem(MainCamera.Camera.ScreenToWorldPoint(Input.mousePosition));
            }
#endif

            if (Application.platform == RuntimePlatform.WindowsPlayer && Input.GetMouseButtonUp(0))
            {
                BuyAndSpawnItem(MainCamera.Camera.ScreenToWorldPoint(Input.mousePosition));
            }
        }

        private bool IsPointerOverUIObject()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                int fingerId = Input.GetTouch(0).fingerId;
                if (EventSystem.current.IsPointerOverGameObject(fingerId)) return true;
            }

#if UNITY_EDITOR
            if (EventSystem.current.IsPointerOverGameObject()) return true;
#endif

            if (Application.platform == RuntimePlatform.WindowsPlayer &&
                EventSystem.current.IsPointerOverGameObject())
                return true;

            return false;
        }

        public void Update()
        {
            CheckClickOnScreen();
        }
    }
}
