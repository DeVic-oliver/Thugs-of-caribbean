namespace Assets.Scripts.Weapons.Guns.Projectiles
{
    using Assets.Scripts.Core.Components.Projectile;
    using Assets.Scripts.Core.Components.Weapon;
    using Assets.Scripts.Player;
    using UnityEngine;
    using System.Collections;

    public class EnemyCannonBall : Projectile
    {

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(_damage);
                _projectileSpeed = 0;
                _sprite.enabled = false;
                RangedWeapon.Pool.BackToPool(this);
            }
        }
        protected override IEnumerator TimerToBackPool()
        {
            float count = 0;
            while (count <= _timeToBackPool)
            {
                count += Time.deltaTime;
                yield return null;
            }
            RangedWeapon.Pool.BackToPool(this);
        }
    }
}