namespace Assets.Scripts.Enemies.Ship.Minion.Shooter
{
    using System.Collections;
    using UnityEngine;
    using Assets.Scripts.Core.Components.Projectile;
    using Devic.Scripts.Utils.Pools;
    using Assets.Scripts.Core.Components.Cannon;
    using UnityEngine.Events;

    public class ShooterCannon : Cannon
    {
        public static CannonBallPool ShooterCannonBallPool;

        public UnityEvent OnShoot;

        [Header("Attributes")]
        [SerializeField] protected float _intervalBetweenShoots;
        [SerializeField] protected int _ammoAmount;
        [SerializeField] protected float _reloadTimeSeconds = 2f;

        private Coroutine _currentCoroutine;


        public override void Shoot()
        {
            if (_currentCoroutine == null)
                _currentCoroutine = StartCoroutine(nameof(ShootProjectile));
        }

        IEnumerator ShootProjectile()
        {
            while (true)
            {
                int shootsMade = 0;
                while (shootsMade < _ammoAmount)
                {
                    shootsMade++;
                    ShootProjectileFromWeaponPosition();
                    OnShoot?.Invoke();
                    yield return new WaitForSeconds(_intervalBetweenShoots);
                }

                float reloadCount = 0;
                while (reloadCount < _reloadTimeSeconds)
                {
                    reloadCount += Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }
            }
        }

        private void ShootProjectileFromWeaponPosition()
        {
            CannonBall projectile = ShooterCannonBallPool.GetProjectileFromPool();
            
            foreach (Transform cannonTransfom in _cannonsTransform)
                projectile.transform.SetPositionAndRotation(cannonTransfom.position, cannonTransfom.rotation);
            
            RemoveParent(projectile.gameObject);
            ShooterCannonBallPool.SetProjectilPoolIfItHasNone(projectile);
        }

        private void RemoveParent(GameObject gameObject)
        {
            if (gameObject.transform.parent != null)
                gameObject.transform.parent = null;
        }

        public void StopShoot()
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
                _currentCoroutine = null;
            }
        }

        private void Awake()
        {
            ShooterCannonBallPool = new(_cannonBall);
        }
        
    }
}