namespace Assets.Scripts.Weapons.Guns
{
    using Assets.Scripts.Core.Components.Projectile;
    using Devic.Scripts.Utils.Pools;
    using System.Collections;
    using UnityEngine;

    public abstract class GunBase : MonoBehaviour
    {
        
        [SerializeField] protected int _ammoMagazineCapacity;
        protected int _currentMagazineAmount;

        [SerializeField] protected float _reloadTime;

        [SerializeField] protected Transform _gunBarrel;
        [SerializeField] protected Projectile _bulletType;

        public bool IsReloading { get; private set; }

        public float AmmoInMagazinePercentage { get; private set; }

        protected ProjectilePool _pool;

        private void Awake()
        {
            _pool = new(_bulletType);
        }

        protected void Start()
        {
            IsReloading = false;
            _currentMagazineAmount = _ammoMagazineCapacity;
            SetPercentageOfMagazineAmount();
        }

        protected virtual void Update()
        {
            AutoReloadIfNoAmmo();
        }
        private void AutoReloadIfNoAmmo()
        {
            if (!CheckIfMagazineHaveAmmo() && !IsReloading)
            {
                IsReloading = true;
                StartCoroutine(ReloadCoroutine());
            }
        }

        public int GetMagazineAmmoAmount()
        {
            return _currentMagazineAmount;
        }

        public virtual void Shoot()
        {
            if(CheckIfMagazineHaveAmmo())
            {
                CreateBullet();
                DecreaseMagazineAmmo();
            }
            else if(!IsReloading)
            {
                IsReloading = true;
                StartCoroutine(ReloadCoroutine());
            }
        }
        public bool CheckIfMagazineHaveAmmo()
        {
            if(_currentMagazineAmount > 0)
            {
                return true;
            }

            return false;
        }
        protected virtual void CreateBullet()
        {
            var bullet = _pool.GetProjectileFromPool();
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            _pool.SetProjectilPoolIfItHasNone(bullet);
        }
        protected void DecreaseMagazineAmmo()
        {
            _currentMagazineAmount--;
            SetPercentageOfMagazineAmount();
        }
        protected void DecreaseMagazineAmmo(int quantityToDecrease)
        {
            _currentMagazineAmount -= quantityToDecrease;
            SetPercentageOfMagazineAmount();
        }
        private void SetPercentageOfMagazineAmount()
        {
            AmmoInMagazinePercentage = GetPercent(_currentMagazineAmount, _ammoMagazineCapacity);
        }
        protected IEnumerator ReloadCoroutine()
        {
            var count = 0f;
            while(count <= _reloadTime)
            {
                count += Time.deltaTime;
                AmmoInMagazinePercentage = GetPercent(count, _reloadTime);
                yield return new WaitForEndOfFrame();
            }
            ReloadMagazine();
            IsReloading = false;
            yield break;
        }
        private void ReloadMagazine()
        {
            _currentMagazineAmount = _ammoMagazineCapacity;
        }
        private float GetPercent(float numberPercentTarget, float totalDivider)
        {
            return ((numberPercentTarget * 100f) / totalDivider) / 100f;
        }
    }
}