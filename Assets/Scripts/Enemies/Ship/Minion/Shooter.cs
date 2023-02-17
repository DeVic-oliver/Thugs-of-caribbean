namespace Assets.Scripts.Enemies.Ships
{
    using Assets.Scripts.Core.Components.Counters;
    using Assets.Scripts.Core.Enemies;
    using Assets.Scripts.Core.Interfaces;
    
    public class Shooter : RangedEnemy, IDamageable, IMoveable, IScoreable
    {
        public bool InstanceScored { get; set; }

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
            CountScore();
        }

        public void CountScore()
        {
            if (!_health.IsAlive && !InstanceScored)
            {
                ScoreCounter.PlayerPoints += _enemyScoreValue;
                InstanceScored = true;
            }
        }
    }
}

