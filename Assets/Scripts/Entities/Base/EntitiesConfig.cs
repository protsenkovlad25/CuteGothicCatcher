using System.Collections.Generic;
using UnityEngine;

namespace CuteGothicCatcher.Entities
{
    [CreateAssetMenu(fileName = "Entities Config", menuName = "Configs/Entities Config")]
    public class EntitiesConfig : ScriptableObject
    {
        [SerializeField] private List<EntityData> m_EntityDatas;
        [SerializeField] private List<PlacedEntityData> m_PlacedEntityDatas;

        public List<EntityData> EntityDatas => m_EntityDatas;
        public List<PlacedEntityData> PlacedEntityDatas => m_PlacedEntityDatas;

        public EntityData GetEntityData(EntityType type, EntitySubType subType = EntitySubType.Ordinary)
        {
            EntityData data = m_EntityDatas.Find(d => d.EntityType == type && d.EntitySubType == subType);

            return data ?? throw new System.NotImplementedException($"Not found entity data of type - {type}");
        }
        public PlacedEntityData GetPlacedEntityData(EntityType type)
        {
            PlacedEntityData data = m_PlacedEntityDatas.Find(d => d.Type == type);

            return data ?? throw new System.NotImplementedException($"Not found recharge entity data of type - {type}");
        }
    }
}
