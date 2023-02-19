namespace Assets.Scripts.Core.Components.Projectile
{
    using Assets.Scripts.Core.Components._2DComponents;
    using Assets.Scripts.Core.Interfaces;
    using Devic.Scripts.Utils.Pools;
    using System.Collections;
    using UnityEngine;

    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] protected float _projectileSpeed;
        protected float _speed;
        [SerializeField] protected int _damage;
        [SerializeField] protected SpriteRenderer _sprite;
        [SerializeField] protected float _timeToBackPool = 1.5f;

        public ProjectilePool MyPool;

        [Header("Audio Setup")]
        [SerializeField] protected AudioClip _firedAudio;
        [SerializeField] protected AudioClip _impactAudio;
        [SerializeField] protected AudioSource _source;

        private Collider2D _collider;

        protected virtual void Awake()
        {
            _speed = _projectileSpeed;
            _source.clip = _firedAudio;
            _collider = GetComponent<Collider2D>();
            _source = GetComponent<AudioSource>();
        }

        protected virtual void Update()
        {
            LaunchProjectile();
        }
        
        protected virtual void LaunchProjectile()
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(_damage);
                DisableComponents();
                _source.PlayOneShot(_impactAudio);
                _explosion.PlaySpriteExplosition();
            }
        }
        protected void DisableComponents()
        {
            _collider.enabled = false;
            _renderer.enabled = false;
            _speed = 0;
        }

        private void OnEnable()
        protected virtual void OnEnable()
        {
            EnableComponents();
            StartOnBackToPool();
        }
        private void EnableComponents()
        {
            _speed = _projectileSpeed;
            _renderer.enabled = true;
            _collider.enabled = true;
            _source.clip = _firedAudio;
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
