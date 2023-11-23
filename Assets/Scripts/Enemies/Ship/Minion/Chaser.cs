using UnityEngine;
using Assets.Scripts.Core.Enemies;
using Assets.Scripts.Core.Interfaces;
using Assets.Scripts.Player;
using Assets.Scripts.Core.Components.Counters;

namespace Assets.Scripts.Enemies.Ships
{
    public class Chaser : MeleeEnemy, IMoveable, IDamageable, IScoreable
    {
        public bool InstanceScored { get; set; }

        public void Move(bool isAlive)
        {
            if (isAlive && base.CheckIfEnemyIsNearby())
            {
                MoveTowardsEnemy();
            }
        }
        
        public void TakeDamage(int damageValue)
        {
            CheckIfDamageKillsInstance(damageValue);
            DecreaseHealthByDamageWithFlashFeedback(damageValue);
        }
        private void CheckIfDamageKillsInstance(int damageValue)
        {
            if(damageValue >= _health.CurrentHealth && !InstanceScored)
            {
                CountScore();
                InstanceScored = true;
            }
        }

        new void Start()
        {
            base.Start();
            InstanceScored = false;
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
            _health.SetCurrentHealthToZero();
        }

        public void CountScore()
        {
            ScoreCounter.PlayerPoints += _enemyScoreValue;
        }
    }
}

