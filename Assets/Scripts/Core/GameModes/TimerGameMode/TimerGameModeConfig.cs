using CuteGothicCatcher.Entities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CuteGothicCatcher.Core
{
    [CreateAssetMenu(fileName = "TimerGameMode Config", menuName = "Configs/GameMode Configs/TimerGameMode Config")]
    public class TimerGameModeConfig : ScriptableObject
    {
        [SerializeField] private float m_TimerTime;
        [SerializeField] private float m_SpawnIntensity;
        [SerializeField] private List<SpawnEntityWeight> m_SpawnEntities;

        public float TimerTime => m_TimerTime;
        public float Intensity => m_SpawnIntensity;
        public List<SpawnEntityWeight> SpawnEntities => m_SpawnEntities;
    }

    [System.Serializable]
    public struct SpawnEntityWeight
    {
        [SerializeField] private EntityType m_EntityType;
        [SerializeField] private float m_Weight;

        public EntityType EntityType => m_EntityType;
        public float Weight => m_Weight;

        public void ChangeWeight(float weight)
        {
            m_Weight = weight;
        }
    }
}
