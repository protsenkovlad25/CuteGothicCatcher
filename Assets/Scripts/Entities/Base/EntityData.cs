using CuteGothicCatcher.Core.Interfaces;
using UnityEngine;

namespace CuteGothicCatcher.Entities
{
    [System.Serializable]
    public class EntityData
    {
        [SerializeField] private string m_TextId;
        [SerializeField] private Sprite m_Sprite;
        [SerializeField] private EntityType m_EntityType;
        [SerializeField] private BaseEntity m_Prefab;
        [SerializeField] private MonoBehaviour m_Spawn;
        [SerializeField] private MonoBehaviour m_Health;
        [SerializeField] private MonoBehaviour m_Movement;
        [SerializeField] private MonoBehaviour m_Collision;
        [SerializeField] private MonoBehaviour m_Clickability;

        public string TextId { get => m_TextId; set => m_TextId = value; }
        public Sprite Sprite { get => m_Sprite; set => m_Sprite = value; }
        public EntityType EntityType { get => m_EntityType; set => m_EntityType = value; }
        public BaseEntity Prefab { get => m_Prefab; set => m_Prefab = value; }
        
        public MonoBehaviour SpawnMB { get => m_Spawn; set => m_Spawn = value; }
        public MonoBehaviour HealthMB { get => m_Health; set => m_Health = value; }
        public MonoBehaviour MovementMB { get => m_Movement; set => m_Movement = value; }
        public MonoBehaviour CollisionMB { get => m_Collision; set => m_Collision = value; }
        public MonoBehaviour ClickabilityMB { get => m_Clickability; set => m_Clickability = value; }

        public ISpawn Spawn => m_Spawn as ISpawn;
        public IHealth Health => m_Health as IHealth;
        public IMoving Movement => m_Movement as IMoving;
        public IColliding Collision => m_Collision as IColliding;
        public IClickable Clickability => m_Clickability as IClickable;
    }
}
