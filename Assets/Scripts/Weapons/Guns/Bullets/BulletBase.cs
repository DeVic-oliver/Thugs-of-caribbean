using Assets.Scripts.Core.Interfaces;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Weapons.Guns
{
    public class BulletBase : MonoBehaviour
    {
        [SerializeField] protected float _bulletSpeed;
        [SerializeField] protected int _damage;


        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector3.up * _bulletSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            if(damageable != null)
            {
                damageable.TakeDamage(_damage);
            }
        }
    }
}