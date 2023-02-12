using Assets.Scripts.Core.Components.Projectile;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Weapons.Guns.Projectiles
{
    [RequireComponent(typeof(CannonBallDisabler))]
    public class EnemyCannonBall : Projectile
    {
        private CannonBallDisabler _disabler;


        private void Start()
        {
            _disabler = GetComponent<CannonBallDisabler>();
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(_damage);
                _projectileSpeed = 0;
                _sprite.enabled = false;
                _disabler.DisableMe();
            }
        }
    }
}