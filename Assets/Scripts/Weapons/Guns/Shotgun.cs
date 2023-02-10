using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Weapons.Guns
{
    public class Shotgun : GunBase
    {
        [SerializeField] private float _spreadAngle = 5;
        private float _bulletsQuantityOnShoot = 4;

        
        protected override void CreateBullet()
        {
            CreateCentralBullet();
            CreateSpreadedBullets();
        }
        private void CreateCentralBullet()
        {
            var bullet = Instantiate(_bulletType, _gunBarrel);
            bullet.transform.localPosition = _gunBarrel.localPosition;
            bullet.transform.localRotation = _gunBarrel.localRotation;
            bullet.transform.parent = null;
        }
        private void CreateSpreadedBullets()
        {
            int spreadMultiplier = 0;
            float spreadAngleAux = _spreadAngle;

            for (int i = 0; i < _bulletsQuantityOnShoot; i++)
            {
                spreadMultiplier = IncrementMultiplierByIndexModZero(spreadMultiplier, i);
                spreadAngleAux = TogglePositiveNegativeByIndexMod(spreadAngleAux, i);
                ShotgunBulletsInstantiation(spreadAngleAux, spreadMultiplier);
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
        private void ShotgunBulletsInstantiation(float spread, int spreadMultiplier)
        {
            var bullet = Instantiate(_bulletType, _gunBarrel);
            bullet.transform.localPosition = _gunBarrel.localPosition;
            bullet.transform.localEulerAngles = Vector3.zero + Vector3.up * (spread * spreadMultiplier);
            bullet.transform.parent = null;
        }
    }
}