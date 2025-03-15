using CuteGothicCatcher.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace CuteGothicCatcher
{
    [CreateAssetMenu(fileName = "Entities Config", menuName = "Configs/Entities Config")]
    public class EntitiesConfig : ScriptableObject
    {
        [SerializeField] private List<EntityData> m_EntityDatas;
        [SerializeField] private List<RechargeEntityData> m_RechargeEntityDatas;

        public List<EntityData> EntityDatas => m_EntityDatas;
        public List<RechargeEntityData> RechargeEntityDatas => m_RechargeEntityDatas;

        public EntityData GetEntityData(EntityType type)
        {
            EntityData data = m_EntityDatas.Find(d => d.EntityType == type);

            return data ?? throw new System.NotImplementedException($"Not found entity data of type - {type}");
        }
        public RechargeEntityData GetRechargeEntityData(EntityType type)
        {
            RechargeEntityData data = m_RechargeEntityDatas.Find(d => d.Type == type);

            return data ?? throw new System.NotImplementedException($"Not found recharge entity data of type - {type}");
        }
    }
}
