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
        [NonSerialized] public Action<int> OnSpendedHearts;

        [SerializeField] private int m_MaxScore;
        [SerializeField] private int m_LastScore;
        [SerializeField] private Dictionary<EntityType, int> m_Items;

        public int Hearts => m_Items[EntityType.Heart];
        public int MaxScore { get => m_MaxScore; set => m_MaxScore = value; }
        public int LastScore { get => m_LastScore; set => m_LastScore = value; }
        public Dictionary<EntityType, int> Items { get => m_Items; set => m_Items = value; }

        public PlayerData()
        {
            m_MaxScore = 0;
            m_LastScore = 0;

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

        public void SpendHearts(int amount)
        {
            int oldCount = m_Items[EntityType.Heart];
            m_Items[EntityType.Heart] -= amount;

            EventManager.ChangedItemCount(EntityType.Heart, oldCount, m_Items[EntityType.Heart]);

            OnSpendedHearts?.Invoke(amount);
            OnChanged?.Invoke();
        }

        public void SetScore(int score)
        {
            m_LastScore = score;

            if (m_LastScore > m_MaxScore)
                m_MaxScore = m_LastScore;

            OnChanged?.Invoke();
        }
    }
}
