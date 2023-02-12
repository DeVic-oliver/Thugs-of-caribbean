using Assets.Scripts.Core.Components.Projectile;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Weapons.Guns.Projectiles
{
    public class EnemyCannonBall : Projectile
    {
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(_damage);
                DisableMe();
            }
        }
    }
}