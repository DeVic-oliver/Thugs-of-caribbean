using Assets.Scripts.Core.Enemies;
using Assets.Scripts.Core.Interfaces;

namespace Assets.Scripts.Enemies.Blob
{
    public class RangedBlobMinion : RangedEnemy, IDamageable, IMoveable
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
            PlayDamageComponentVFX("bloodSpill");
        }

        new void Start()
        {
            base.Start();
        }

        new void Update()
        {
            base.Update();
            Move(IsAlive);
        }
    }
}

