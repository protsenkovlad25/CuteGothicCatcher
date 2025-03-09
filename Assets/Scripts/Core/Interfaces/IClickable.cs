using CuteGothicCatcher.Entities;

namespace CuteGothicCatcher.Core.Interfaces
{
    public interface IClickable
    {
        void Init(BaseEntity self);
        void Click(BaseEntity self);
        void DisableClickability();
    }
}
