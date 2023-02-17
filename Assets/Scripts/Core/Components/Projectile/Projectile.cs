namespace Assets.Scripts.Core.Components.Projectile
{
    using Assets.Scripts.Core.Interfaces;
    using Devic.Scripts.Utils.Pools;
    using System.Collections;
    using UnityEngine;

    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] protected float _projectileSpeed;
        [SerializeField] protected int _damage;
        [SerializeField] protected SpriteRenderer _sprite;
        [SerializeField] protected float _timeToBackPool = 1.5f;

        public ProjectilePool MyPool;


        protected virtual void Update()
        {
            LaunchProjectile();
        }
        
        protected virtual void LaunchProjectile()
        {
            transform.Translate(Vector3.up * _projectileSpeed * Time.deltaTime);
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(_damage);
                MyPool.BackToPool(this);
            }
        }

        private void OnEnable()
        {
            StartOnBackToPool();
        }

        protected virtual void StartOnBackToPool()
        {
            StartCoroutine("TimerToBackPool");
        }
        protected virtual IEnumerator TimerToBackPool()
        {
            float count = 0;
            while (count <= _timeToBackPool)
            {
                count += Time.deltaTime;
                yield return null;
            }
            MyPool.BackToPool(this);
        }
    }
}
