using CuteGothicCatcher.Core.Interfaces;
using CuteGothicCatcher.Objects;
using System.Collections.Generic;
using UnityEngine;

namespace CuteGothicCatcher.Core
{
    public class GameContent : MonoBehaviour, IIniting
    {
        [Header("Objects")]
        [SerializeField] private GameObject m_EntitiesParent;
        [SerializeField] private List<Border> m_Borders;

        public void Init()
        {
            foreach (var border in m_Borders)
                border.Init();
        }
    }
}
