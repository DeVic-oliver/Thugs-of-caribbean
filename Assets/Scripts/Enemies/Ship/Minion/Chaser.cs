using UnityEngine;
using Assets.Scripts.Core.Enemies;
using Assets.Scripts.Core.Interfaces;
using Assets.Scripts.Player;
using Assets.Scripts.Core.Components.Counters;

namespace Assets.Scripts.Enemies.Ships
{
    public class Chaser : MeleeEnemy, IMoveable, IDamageable, IScoreable
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
        
        private void Update()
        {
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

        public void CountScore()
        {
            ScoreCounter.PlayerPoints += _enemyScoreValue;
        }
    }
}

