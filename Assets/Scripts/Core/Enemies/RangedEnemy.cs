using System.Collections;
using UnityEngine;
using Assets.Scripts.Core.Components.Weapon;

namespace Assets.Scripts.Core.Enemies
{
    [RequireComponent(typeof(RangedWeapon))]
    public abstract class RangedEnemy : EnemyBase
    {
        protected RangedWeapon _weapon;
        protected bool _isShooting;

        protected override void Start()
        {
            base.Start();
            _weapon = GetComponent<RangedWeapon>();
        }

        protected override void Update()
        {
            base.Update();
            ShootIfEnemyIsNearby();
        }

        protected void ShootIfEnemyIsNearby()
        {
            if (CheckIfEnemyIsNearby())
            {
                _isShooting = true;
                _weapon.Shoot();
            }
            else if(!CheckIfEnemyIsNearby())
            {
                _isShooting = false;
                _weapon.StopShoot();
            }
        }
    }
}