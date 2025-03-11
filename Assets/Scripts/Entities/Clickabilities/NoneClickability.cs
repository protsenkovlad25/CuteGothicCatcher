using CuteGothicCatcher.Core.Interfaces;
using UnityEngine;

namespace CuteGothicCatcher.Entities.Components
{
    public class NoneClickability : MonoBehaviour, IClickable
    {
        public event IClickable.Clicked OnClicked;

        public void Init(BaseEntity self)
        {
        }

        public void Click(BaseEntity self)
        {
        }

        public void DisableClickability()
        {
        }
    }
}
