using Assets.Scripts.Core.Interfaces;
using System.Collections;
using System.Security;
using UnityEngine;

namespace Assets.Scripts.Core.Components.Projectile
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] protected float _projectileSpeed;
        [SerializeField] protected int _damage;
        [SerializeField] protected SpriteRenderer _sprite;


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
            }
        }
    }
}
