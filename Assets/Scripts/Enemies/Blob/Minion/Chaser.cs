using UnityEngine;
using Assets.Scripts.Core.Enemies;
using Assets.Scripts.Core.Interfaces;
using DG.Tweening;

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
            Move(IsAlive);
        }
    }
}

