namespace CuteGothicCatcher.Core.Interfaces
{
    public interface IHealth : IDamageable, IHealing, IIniting
    {
        public delegate void TakedDamage(float damagePoints);
        public delegate void Healed(float healPoints);
        public delegate void Died();

        public event TakedDamage OnTakedDamage;
        public event Healed OnHealed;
        public event Died OnDie;

        void ResetHealth();
        float GetMaxHealth();
        float GetCurrentHealth();
    }
}
