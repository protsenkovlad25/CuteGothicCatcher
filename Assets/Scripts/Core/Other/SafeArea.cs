using UnityEngine;

namespace CuteGothicCatcher.Core
{
    public class SafeArea : MonoBehaviour
    {
        [SerializeField] private RectTransform m_BlackSafeArea;

        private void Awake()
        {
            //UpdateSafeArea();
        }

        private void UpdateSafeArea()
        {
            Rect safeArea = Screen.safeArea;
            RectTransform transform = GetComponent<RectTransform>();

            Vector2 anchorMin = safeArea.position;
            Vector2 anchorMax = safeArea.position + safeArea.size;

            Debug.LogError($"SW - {Screen.width}\n" +
                $"SH - {Screen.height}\n" +
                $"Anch Min - {anchorMin}\n" +
                $"Anch Max - {anchorMax}");

            Debug.LogError($"SA Pos - {safeArea.position}\n" +
                $"SA Size - {safeArea.size}");

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;

            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            transform.anchorMin = anchorMin;
            transform.anchorMax = anchorMax;

            m_BlackSafeArea.gameObject.SetActive(true);
            m_BlackSafeArea.anchorMin = anchorMax;

            Debug.LogError($"Anch Min - {anchorMin}\n" +
                $"Anch Max - {anchorMax}");
        }
    }
}
