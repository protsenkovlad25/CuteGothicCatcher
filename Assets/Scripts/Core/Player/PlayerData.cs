using CuteGothicCatcher.Entities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CuteGothicCatcher.Core.Player
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] private Dictionary<EntityType, int> m_Items;

        public Dictionary<EntityType, int> Items { get => m_Items; set => m_Items = value; }

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
        }
    }
}
