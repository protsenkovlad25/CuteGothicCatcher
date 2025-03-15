using CuteGothicCatcher.Core;
using UnityEngine;

namespace CuteGothicCatcher.Entities.Components
{
    public class CameraAreaSpawn : BaseSpawn
    {
        [SerializeField] private float m_Offset;

        public override void Spawn(Transform transform, Transform model)
        {
            Vector2 screenMin = MainCamera.ScreenMin + new Vector2(m_Offset, m_Offset);
            Vector2 screenMax = MainCamera.ScreenMax - new Vector2(m_Offset, m_Offset);

            float randomX = Random.Range(screenMin.x, screenMax.x);
            float randomY = Random.Range(screenMin.y, screenMax.y);

            transform.position = new Vector2(randomX, randomY);

            base.Spawn(transform, model);
        }
    }
}
