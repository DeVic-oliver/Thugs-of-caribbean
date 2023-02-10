using Assets.Scripts.Core.Enemies;
using Assets.Scripts.Core.Interfaces;

namespace Assets.Scripts.Enemies.Blob.Boss
{
    public class MeleeBlobBoss : MeleeEnemy, IMoveable, IDamageable
    {
        public void Move(bool isAlive)
        {
            if(isAlive)
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
            Move(true);
        }
    }
}