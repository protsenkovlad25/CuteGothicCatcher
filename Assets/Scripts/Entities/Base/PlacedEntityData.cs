using UnityEngine;

namespace CuteGothicCatcher.Entities
{
    [System.Serializable]
    public class PlacedEntityData
    {
        [SerializeField] private string m_Name;
        [SerializeField] private EntityType m_Type;
        [SerializeField] private float m_RechargeTime;
        [SerializeField] private int m_Price;

        public EntityType Type => m_Type;
        public float RechargeTime => m_RechargeTime;
        public int Price => m_Price;
    }
}
