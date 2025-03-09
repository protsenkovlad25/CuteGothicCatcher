using CuteGothicCatcher.Core.Interfaces;
using CuteGothicCatcher.Objects;
using UnityEngine;

namespace CuteGothicCatcher.Entities.Components
{
    public class DestroyClickability : MonoBehaviour, IClickable
    {
        [SerializeField] private float m_Damage;
        [SerializeField] private CracksEffectSprite m_CrackEffectPrefab;

        private CracksEffectSprite m_CrackEffect;

        public void Init(BaseEntity self)
        {
            SetCrackEffect(self);
        }

        public void Click(BaseEntity self)
        {
            self.TakeDamage(m_Damage);
        }

        public void DisableClickability()
        {
            m_CrackEffect.OnDeactivate();
        }

        private void SetCrackEffect(BaseEntity self)
        {
            m_CrackEffect = Instantiate(m_CrackEffectPrefab, transform);
            m_CrackEffect.OnDeactivate();

            self.Data.Health.OnTakedDamage += (amount) => { UpdateCracks(self.GetCurrentHealth(), self.GetMaxHealth()); };
        }
        private void UpdateCracks(float curHealth, float maxHealth)
        {
            m_CrackEffect.UpdateCracks(curHealth, maxHealth);
        }
    }
}
