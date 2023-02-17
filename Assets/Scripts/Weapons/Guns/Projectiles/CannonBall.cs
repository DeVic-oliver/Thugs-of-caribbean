using Assets.Scripts.Core.Components.Projectile;
using Assets.Scripts.Weapons.Guns.Projectiles;
using UnityEngine;

namespace Assets.Scripts.Weapons.Projectiles
{
    public class CannonBall : Projectile
    {
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
        }

    }
}