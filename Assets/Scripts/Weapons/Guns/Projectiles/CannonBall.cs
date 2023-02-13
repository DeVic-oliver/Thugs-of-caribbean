using Assets.Scripts.Core.Components.Projectile;
using Assets.Scripts.Weapons.Guns.Projectiles;
using UnityEngine;

namespace Assets.Scripts.Weapons.Projectiles
{
    [RequireComponent(typeof(CannonBallDisabler))]
    public class CannonBall : Projectile
    {
        private CannonBallDisabler _disabler;

        private void Start()
        {
            _disabler = GetComponent<CannonBallDisabler>();
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);

            _disabler.DisableMe();
        }

    }
}