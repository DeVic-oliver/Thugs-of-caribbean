namespace Assets.Scripts.Core.Components.Weapon
{
    using System.Collections;
    using UnityEngine;
    using Assets.Scripts.Core.Components.Projectile;
    using Devic.Scripts.Utils.Pools;

    public class RangedWeapon : MonoBehaviour
    {
        [Header("Weapon Setup")]
        [SerializeField] protected Transform _weaponPosition;
        [SerializeField] protected Projectile _projectile;
        [SerializeField] protected float _intervalBetweenShoots;
        [SerializeField] protected int _ammoAmount;
        [SerializeField] protected float _reloadTimeSeconds = 2f;

        private Coroutine _currentCoroutine;

        public static ProjectilePool Pool;

        private void Awake()
        {
            Pool = new(_projectile);
        }

        public void Shoot()
        {
            if (_currentCoroutine == null)
            {
                _currentCoroutine = StartCoroutine(ShootProjectile());
            }
        }
        public void StopShoot()
        {
            if(_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
                _currentCoroutine = null;
            }
        }
        IEnumerator ShootProjectile()
        {
            while (true)
            {
                int shootsMade = 0;
                float reloadCount = 0;
                while (shootsMade < _ammoAmount)
                {
                    shootsMade++;
                    ShootProjectileFromWeaponPosition();
                    yield return new WaitForSeconds(_intervalBetweenShoots);
                }
            
                while(reloadCount < _reloadTimeSeconds)
                {
                    reloadCount += Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }
                shootsMade = 0;
            }
        }
        protected virtual void ShootProjectileFromWeaponPosition()
        {
            var projectile = Pool.GetProjectileFromPool();
            projectile.transform.position = _weaponPosition.position;
            projectile.transform.rotation = _weaponPosition.rotation;
            RemoveParent(_projectile.gameObject);
            Pool.SetProjectilPoolIfItHasNone(projectile);
        }
        private void RemoveParent(GameObject gameObject)
        {
            if(gameObject.transform.parent != null) 
            {
                gameObject.transform.parent = null;
            }
        }
    }
}