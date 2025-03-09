using UnityEngine;

namespace CuteGothicCatcher.Core
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private GameManager m_GameManager;

        private void Awake()
        {
            Time.timeScale = 1;

            MainCamera.Init();

            m_GameManager.Init();
        }
    }
}
