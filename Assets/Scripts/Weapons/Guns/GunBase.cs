using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Weapons.Guns
{
    public abstract class GunBase : MonoBehaviour
    {
        // A gun can have states to divide funcionalities. (FIRING, RELOADING, IDLE, EMPTY)
        //Should create those states?
        
        [SerializeField] protected int _ammoMagazineCapacity;
        protected int _currentMagazineAmount;

        [SerializeField] protected float _reloadTime;


        [SerializeField] protected Transform _gunBarrel;
        [SerializeField] protected BulletBase _bulletType;

        public bool IsReloading { get; private set; }

        public float AmmoInMagazinePercentage { get; private set; }

        protected void Start()
        {
            IsReloading = false;
            _currentMagazineAmount = _ammoMagazineCapacity;
            SetPercentageOfMagazineAmount();
        }

        public int GetMagazineAmmoAmount()
        {
            return _currentMagazineAmount;
        }


        public virtual void Shoot()
        {
            if(MagazineHaveAmmo())
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
        public bool MagazineHaveAmmo()
        {
            if(_currentMagazineAmount > 0)
            {
                return true;
            }

            return false;
        }
        protected virtual void CreateBullet()
        {
            var bullet = Instantiate(_bulletType);
            bullet.transform.position = _gunBarrel.position;
            bullet.transform.rotation = _gunBarrel.rotation;
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
            Debug.Log($"Gun fully reloaded: {_currentMagazineAmount} ammo in magazine");
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