using Assets.Scripts.Core.Components.Counters;
using Assets.Scripts.Core.Enemies;
using Assets.Scripts.Core.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Enemies.Ships
{
    public class Shooter : RangedEnemy, IDamageable, IMoveable, IScoreable
    {

        public void Move(bool isAlive)
        {
            if (isAlive && base.CheckIfEnemyIsNearby())
            {
                LookToTargetSmoothly();
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

        public void CountScore()
        {
            ScoreCounter.PlayerPoints += _enemyScoreValue;
        }
    }
}

