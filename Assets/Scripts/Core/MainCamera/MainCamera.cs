using UnityEngine;

namespace CuteGothicCatcher.Core
{
    public static class MainCamera
    {
        private static bool m_IsInited;

        private static Camera m_Camera;
        private static Vector2 m_ViewportMin;
        private static Vector2 m_ViewportMax;
        private static Vector2 m_ScreenMin;
        private static Vector2 m_ScreenMax;

        public static Camera Camera => m_Camera;
        public static Vector2 ViewportMin => m_ViewportMin;
        public static Vector2 ViewportMax => m_ViewportMax;
        public static Vector2 ScreenMin => m_ScreenMin;
        public static Vector2 ScreenMax => m_ScreenMax;
        public static float ScreenWidth => Screen.width;
        public static float ScreenHeight => Screen.height;

        public static void Init()
        {
            if (!m_IsInited)
            {
                m_IsInited = true;

                m_Camera = Camera.main;

                m_ViewportMin = m_Camera.ViewportToWorldPoint(Vector2.zero);
                m_ViewportMax = m_Camera.ViewportToWorldPoint(Vector2.one);

                m_ScreenMin = m_Camera.ScreenToWorldPoint(Vector2.zero);
                m_ScreenMax = m_Camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            }
        }
    }
}
