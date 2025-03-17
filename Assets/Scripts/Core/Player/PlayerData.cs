using CuteGothicCatcher.Entities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CuteGothicCatcher.Core.Player
{
    [Serializable]
    public class PlayerData
    {
        [NonSerialized] public Action OnChanged;

        [SerializeField] private Dictionary<EntityType, int> m_Items;

        public Dictionary<EntityType, int> Items { get => m_Items; set => m_Items = value; }
        public int Hearts => m_Items[EntityType.Heart];

        public PlayerData()
        {
            m_Items = new Dictionary<EntityType, int>();

            Load();
        }

        public void Load()
        {
            foreach (EntityType type in Enum.GetValues(typeof(EntityType)))
            {
                if (!m_Items.ContainsKey(type))
                    m_Items.Add(type, 0);
            }

            EventManager.OnCollectEntity.AddListener(CollectEntity);
        }

        private void CollectEntity(EntityType type, float points)
        {
            int oldCount = m_Items[type];
            m_Items[type]++;

            EventManager.ChangedItemCount(type, oldCount, m_Items[type]);

            OnChanged?.Invoke();
        }

        public void ClearItems()
        {
            foreach (EntityType type in Enum.GetValues(typeof(EntityType)))
            {
                m_Items[type] = 0;

                if (type != EntityType.None)
                    EventManager.ChangedItemCount(type, 0, 0);
            }

            OnChanged?.Invoke();
        }

        public void SpendHearts(int amout)
        {
            int oldCount = m_Items[EntityType.Heart];
            m_Items[EntityType.Heart] -= amout;

            EventManager.ChangedItemCount(EntityType.Heart, oldCount, m_Items[EntityType.Heart]);

            OnChanged?.Invoke();
        }
    }
}
