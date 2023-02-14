using Assets.Scripts.Core.Components;
using Assets.Scripts.Core.Components.Damage;
using Assets.Scripts.Core.Interfaces;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerHealth : Health, IDamageable
    {
        private DamageComponent _damageComponent;

        public void TakeDamage(int damageValue)
        {
            DecreaseHealth(damageValue);
            _damageComponent.FlashShader();
        }

        new void Start()
        {
            base.Start();
            _damageComponent = GetComponent<DamageComponent>();
        }

        new void Update()
        {
            base.Update();
            PlayDeathVFXWhenDie();
        }
        private void PlayDeathVFXWhenDie()
        {
            if (!IsAlive && !HasDied)
            {
                HasDied = true;
                _damageComponent.PlayDeathVFX();
            }
        }
    }
}