using UnityEngine;

namespace CuteGothicCatcher.Entities
{
    [System.Serializable]
    public class RechargeEntityData
    {
        [SerializeField] private string m_Name;
        [SerializeField] private EntityType m_Type;
        [SerializeField] private float m_RechargeTime;

        public EntityType Type => m_Type;
        public float RechargeTime => m_RechargeTime;
    }
}
