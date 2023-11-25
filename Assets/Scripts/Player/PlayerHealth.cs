namespace Assets.Scripts.Player
{
    using Assets.Scripts.Core.Components;
    using Assets.Scripts.Core.Components.Damage;
    using Assets.Scripts.Core.Interfaces;
    

    public class PlayerHealth : Health, IDamageable
    {
        private VisualDamageFeedback _damageComponent;

        public void TakeDamage(int damageValue)
        {
            DecreaseHealth(damageValue);
            _damageComponent.FlashShader();
        }

        private void Start()
        {
            _damageComponent = GetComponent<VisualDamageFeedback>();
        }

        new void Update()
        {
            base.Update();
            PlayDeathVFXWhenDie();
        }

        private void PlayDeathVFXWhenDie()
        {
            if (!IsAlive && !HasJustDied)
            {
                HasJustDied = true;
                _damageComponent.PlayDeathVFX();
            }
        }
    }
}