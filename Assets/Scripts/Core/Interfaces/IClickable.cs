using CuteGothicCatcher.Entities;

namespace CuteGothicCatcher.Core.Interfaces
{
    public interface IClickable
    {
        delegate void Clicked();

        event Clicked OnClicked;

        void Init(BaseEntity self);
        void Click(BaseEntity self);
        void DisableClickability();
    }
}
