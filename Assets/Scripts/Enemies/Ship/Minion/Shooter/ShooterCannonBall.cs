namespace Assets.Scripts.Enemies.Ship.Minion.Shooter
{
    using Assets.Scripts.Core.Components.Damage;
    using Assets.Scripts.Core.Components.Projectile;
    using System.Collections;
    using UnityEngine;
    

    public class ShooterCannonBall : CannonBall
    {
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                DamageGateway damageGateway = other.GetComponent<DamageGateway>();
                damageGateway.ApplyDamageOnHealth(_damage);
                DisableVisualAndCollisionComponents();
                _source.PlayOneShot(_impactAudio);
                _explosion.PlaySpriteExplosion();
                _currentSpeed = 0;
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
            ShooterCannon.ShooterCannonBallPool.BackToPool(this);
        }
    }
}