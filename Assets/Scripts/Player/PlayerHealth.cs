namespace Assets.Scripts.Player
{
    using Assets.Scripts.Core.Components;
    using Assets.Scripts.Core.Components.Damage;
    

    public class PlayerHealth : Health
    {
        private VisualDamageFeedback _damageComponent;


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