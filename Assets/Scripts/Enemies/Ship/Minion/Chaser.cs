using UnityEngine;
using Assets.Scripts.Core.Enemies;
using Assets.Scripts.Core.Interfaces;
using DG.Tweening;
using Assets.Scripts.Player;

namespace Assets.Scripts.Enemies.Ships
{
    public class Chaser : MeleeEnemy, IMoveable, IDamageable
    {
        public void Move(bool isAlive)
        {
            if (isAlive && base.CheckIfEnemyIsNearby())
            {
                MoveTowardsEnemy();
            }
        }
        
        public void TakeDamage(int damageValue)
        {
            DecreaseHealthByDamageWithFlashFeedback(damageValue);
        }

        new void Start()
        {
            base.Start();
        }
        
        new void Update()
        {
            base.Update();
            Move(_health.IsAlive);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
            if(player != null)
            {
                player.TakeDamage((int)_attackDamage);
                DestroyMyself();
            }
        }
        private void DestroyMyself()
        {
            _health.KillMe();
        }
    }
}

