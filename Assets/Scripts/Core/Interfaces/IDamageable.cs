using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CuteGothicCatcher.Core.Interfaces
{
    public interface IDamageable
    {
        void TakeDamage(float amount);
        void Die();
    }
}
