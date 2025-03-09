using CuteGothicCatcher.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace CuteGothicCatcher
{
    [CreateAssetMenu(fileName = "Entities Config", menuName = "Configs/Entities Config")]
    public class EntitiesConfig : ScriptableObject
    {
        [SerializeField] private List<EntityData> m_EntityDatas;

        public List<EntityData> EntityDatas => m_EntityDatas;

        public EntityData GetData(EntityType type)
        {
            EntityData data = m_EntityDatas.Find(d => d.EntityType == type);

            return data ?? throw new System.NotImplementedException($"Not found entity data of type - {type}");
        }
    }
}
