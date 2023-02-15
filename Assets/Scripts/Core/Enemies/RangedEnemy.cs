using System.Collections;
using UnityEngine;
using Assets.Scripts.Core.Components.Weapon;
using Assets.Scripts.Utils.SightRaycast._2D;

namespace Assets.Scripts.Core.Enemies
{
    [RequireComponent(typeof(RangedWeapon))]
    public abstract class RangedEnemy : EnemyBase
    {
        protected RangedWeapon _weapon;
        protected bool _isShooting;
        protected bool _isEnemyOnSight;

        protected string _enemyLayerMask = "Player";

        protected override void Start()
        {
            base.Start();
            _weapon = GetComponent<RangedWeapon>();
        }

        protected void Update()
        {
            ShootIfEnemyIsNearby();
        }

        protected void ShootIfEnemyIsNearby()
        {
            _isEnemyOnSight = SightRaycaster2D.CheckGameObjectOnSight(transform, _rangeDetection, _enemyLayerMask);

            if (CheckIfEnemyIsNearby() && _isEnemyOnSight)
            {
                _isShooting = true;
                _weapon.Shoot();
            }
            else if(!CheckIfEnemyIsNearby() || !_health.IsAlive)
            {
                _isShooting = false;
                _weapon.StopShoot();
            }
        }
    }
}