using CuteGothicCatcher.Core;
using CuteGothicCatcher.Core.Interfaces;
using UnityEngine;

namespace CuteGothicCatcher.Entities.Components
{
    public class CameraSideSpawn : MonoBehaviour, ISpawn
    {
        [SerializeField] private float m_Offset;

        public void Init()
        {
        }

        public void Spawn(Transform transform)
        {
            Vector2 screenMin = MainCamera.ViewportMin;
            Vector2 screenMax = MainCamera.ViewportMax;

            int side = Random.Range(0, 4);
            Vector2 spawnPos = Vector2.zero;

            switch (side)
            {
                case 0: // Слева
                    spawnPos = new Vector2(screenMin.x - m_Offset, Random.Range(screenMin.y, screenMax.y));
                    break;
                case 1: // Справа
                    spawnPos = new Vector2(screenMax.x + m_Offset, Random.Range(screenMin.y, screenMax.y));
                    break;
                case 2: // Снизу
                    spawnPos = new Vector2(Random.Range(screenMin.x, screenMax.x), screenMin.y - m_Offset);
                    break;
                case 3: // Сверху
                    spawnPos = new Vector2(Random.Range(screenMin.x, screenMax.x), screenMax.y + m_Offset);
                    break;
            }

            transform.position = spawnPos;
        }
    }
}
