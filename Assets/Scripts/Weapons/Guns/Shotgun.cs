using Assets.Scripts.Core.Components.Projectile;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Weapons.Guns
{
    public class Shotgun : GunBase
    {
        [SerializeField] private float _spreadAngle = 5;
        private int _bulletsQuantityOnShoot = 2;

        
        protected override void CreateBullet()
        {
            CreateCentralBullet();
            CreateSpreadedBullets();
        }
        private void CreateCentralBullet()
        {
            var bullet = _pool.GetProjectileFromPool();
            SetProjectileTransformBasedOnParent(bullet, _gunBarrel);
            SetProjectilPoolIfItHasNone(bullet);
        }

        private void SetProjectilPoolIfItHasNone(Projectile projectile) 
        {
            if (projectile.MyPool == null)
            {
                projectile.MyPool = _pool;
            }
        }

        private void CreateSpreadedBullets()
        {
            int spreadMultiplier = 0;
            float spreadAngleAux = _spreadAngle;

            var bullets = _pool.GetProjectileFromPool(_bulletsQuantityOnShoot);

            for (int i = 0; i < bullets.Count; i++)
            {
                spreadMultiplier = IncrementMultiplierByIndexModZero(spreadMultiplier, i);
                spreadAngleAux = TogglePositiveNegativeByIndexMod(spreadAngleAux, i);
                var projectileRotation = GetRotationForSpreadProjectile(spreadAngleAux, spreadMultiplier);
                SetProjectileTransformBasedOnParent(bullets[i], _gunBarrel, projectileRotation);
                SetProjectilPoolIfItHasNone(bullets[i]);
            }
        }
        private int IncrementMultiplierByIndexModZero(int multiplier, int index)
        {
            if(index % 2 == 0)
            {
                return ++multiplier;
            }

            return multiplier;
        }
        private float TogglePositiveNegativeByIndexMod(float number, int index) 
        {
            if(index % 2 == 0)
            {
                return Math.Abs(number);
            }

            return number * -1;
        }
        private Vector3 GetRotationForSpreadProjectile(float angle, float multiplier)
        {
            return Vector3.zero + Vector3.forward * (angle * multiplier);
        }

        private void SetProjectileTransformBasedOnParent(Projectile projectile, Transform parent)
        {
            projectile.transform.SetParent(parent);
            projectile.transform.localPosition = parent.localPosition;
            projectile.transform.localRotation = parent.localRotation;
            projectile.transform.SetParent(null);
        }
        private void SetProjectileTransformBasedOnParent(Projectile projectile, Transform parent, Vector3 eulers)
        {
            projectile.transform.SetParent(parent);
            projectile.transform.localPosition = parent.localPosition;
            projectile.transform.localEulerAngles = eulers;
            projectile.transform.SetParent(null);
        }
    }
}