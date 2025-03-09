using CuteGothicCatcher.Core.Interfaces;
using UnityEngine;

namespace CuteGothicCatcher.Objects
{
    public enum ScreenEdge
    {
        Left, Right, Top, Bottom
    }

    public class Border : MonoBehaviour, IIniting
    {
        [SerializeField] private ScreenEdge m_Edge;
        [SerializeField] private float m_Thickness = 1f;

        private void OnValidate()
        {
            SetPosition();
        }

        public void Init()
        {
            SetPosition();
        }

        private void SetPosition()
        {
            Camera cam = Camera.main;

            Vector3 newPosition = transform.position;
            Vector3 newScale = transform.localScale;

            float screenHeight = cam.orthographicSize * 2; // Высота экрана в мировых координатах
            float screenWidth = screenHeight * cam.aspect; // Ширина экрана в мировых координатах

            switch (m_Edge)
            {
                case ScreenEdge.Left:
                    newPosition = cam.ViewportToWorldPoint(new Vector3(0, 0.5f, cam.nearClipPlane));
                    newPosition.x -= m_Thickness / 2;
                    newScale = new Vector3(m_Thickness, screenHeight, 1);
                    break;

                case ScreenEdge.Right:
                    newPosition = cam.ViewportToWorldPoint(new Vector3(1, 0.5f, cam.nearClipPlane));
                    newPosition.x += m_Thickness / 2;
                    newScale = new Vector3(m_Thickness, screenHeight, 1);
                    break;

                case ScreenEdge.Top:
                    newPosition = cam.ViewportToWorldPoint(new Vector3(0.5f, 1, cam.nearClipPlane));
                    newPosition.y += m_Thickness / 2;
                    newScale = new Vector3(screenWidth, m_Thickness, 1);
                    break;

                case ScreenEdge.Bottom:
                    newPosition = cam.ViewportToWorldPoint(new Vector3(0.5f, 0, cam.nearClipPlane));
                    newPosition.y -= m_Thickness / 2;
                    newScale = new Vector3(screenWidth, m_Thickness, 1);
                    break;
            }

            newPosition.z = 0;

            transform.position = newPosition;
            transform.localScale = newScale;
        }
    }
}
