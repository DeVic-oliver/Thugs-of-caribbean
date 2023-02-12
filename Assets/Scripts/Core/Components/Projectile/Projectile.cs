using Assets.Scripts.Core.Interfaces;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Core.Components.Projectile
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] protected float _projectileSpeed;
        [SerializeField] protected int _damage;

        private float _timeToDisable = 1.5f;

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
                DisableMe();
            }
        }
        public void DisableMe()
        {
            _projectileSpeed = 0;
            StartCoroutine("DisableInstance");
        }
        private IEnumerator DisableInstance()
        {
            float count = 0;
            while(count < _timeToDisable)
            {
                count += Time.deltaTime;
                yield return null;
            }
            gameObject.SetActive(false);
        }
    }
}
