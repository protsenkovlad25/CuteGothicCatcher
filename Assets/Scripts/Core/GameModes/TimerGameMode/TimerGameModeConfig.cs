using CuteGothicCatcher.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace CuteGothicCatcher.Core
{
    [CreateAssetMenu(fileName = "TimerGameMode Config", menuName = "Configs/GameMode Configs/TimerGameMode Config")]
    public class TimerGameModeConfig : ScriptableObject
    {
        [Header("Timer time (minutes)")]
        [Range(1, 10)]
        [SerializeField] private int m_TimerTime;

        [Header("Spawn Values")]
        [Range(0f, 1f)]
        [SerializeField] private float m_SpawnIntensity;
        
        [Header("Multipliers for weights after spawn")]
        [SerializeField] private float m_MultiplierSpawnedEntity;
        [SerializeField] private float m_MultiplierOtherEntity;

        [Header("Range of multiple spawn count")]
        [SerializeField] private int m_MinMultipleSpawnCount;
        [SerializeField] private int m_MaxMultipleSpawnCount;

        [Header("Range of multiple spawn chance")]
        [Range(0f, 1f)]
        [SerializeField] private float m_MinMultipleSpawnChance;
        [Range(0f, 1f)]
        [SerializeField] private float m_MaxMultipleSpawnChance;

        [Header("Spawn entities")]
        [SerializeField] private List<SpawnEntityWeight> m_SpawnEntities;

        public float TimerTime => m_TimerTime * 60;
        public float Intensity => m_SpawnIntensity;
        public float MultiplierSpawnedEntity => m_MultiplierSpawnedEntity;
        public float MultiplierOtherEntity => m_MultiplierOtherEntity;
        public int MinMultipleSpawnCount => m_MinMultipleSpawnCount;
        public int MaxMultipleSpawnCount => m_MaxMultipleSpawnCount;
        public float MinMultipleSpawnChance => m_MinMultipleSpawnChance;
        public float MaxMultipleSpawnChance => m_MaxMultipleSpawnChance;
        public List<SpawnEntityWeight> SpawnEntities => m_SpawnEntities;

        private void OnValidate()
        {
            if (m_MinMultipleSpawnChance > m_MaxMultipleSpawnChance)
                m_MaxMultipleSpawnChance = m_MinMultipleSpawnChance + 0.01f;
        }
    }

    [System.Serializable]
    public struct SpawnEntityWeight
    {
        [SerializeField] private EntityType m_EntityType;
        [SerializeField] private EntitySubType m_EntitySubType;
        [SerializeField] private float m_Weight;
        [SerializeField] private int m_MaxNum;

        public EntityType EntityType => m_EntityType;
        public EntitySubType EntitySubType => m_EntitySubType;
        public float Weight => m_Weight;
        public int MaxNum => m_MaxNum;

        public void ChangeWeight(float weight)
        {
            m_Weight = weight;
        }
    }
}
