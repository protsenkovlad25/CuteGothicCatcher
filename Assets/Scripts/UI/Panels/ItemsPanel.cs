using CuteGothicCatcher.Entities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CuteGothicCatcher.Core;
using DG.Tweening;
using UnityEngine.Events;

namespace CuteGothicCatcher.UI
{
    public class ItemsPanel : Panel
    {
        [Header("Objects")]
        [SerializeField] private VerticalLayoutGroup m_Layout;
        [SerializeField] private ItemSlot m_SlotPrefab;

        private Dictionary<EntityType, ItemSlot> m_Slots;

        public override void Init()
        {
            base.Init();

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
        
        #region Anim Methods
        protected override void OpenAnim(UnityAction onEndAction = null)
        {
            gameObject.SetActive(true);

            m_CloseSeq?.Kill();

            Sequence openSeq = DOTween.Sequence();
            m_OpenSeq = openSeq;

            openSeq.Append(m_RectTransform.DOAnchorPos(m_OpenPos, m_OpenTime));

            if (onEndAction != null)
                openSeq.AppendCallback(onEndAction.Invoke);

            openSeq.SetUpdate(true);
        }
        protected override void CloseAnim(UnityAction onEndAction = null)
        {
            m_OpenSeq?.Kill();

            Sequence closeSeq = DOTween.Sequence();
            m_CloseSeq = closeSeq;

            closeSeq.Append(m_RectTransform.DOAnchorPos(m_ClosePos, m_CloseTime));
            closeSeq.AppendCallback(() => { gameObject.SetActive(false); });

            if (onEndAction != null)
                closeSeq.AppendCallback(onEndAction.Invoke);

            closeSeq.SetUpdate(true);
        }
        #endregion
    }
}
