namespace Assets.Scripts.Core.Components.Projectile
{
    using Assets.Scripts.Core.Components._2DComponents;
    using Assets.Scripts.Core.Components.Damage;
    using Devic.Scripts.Utils.Pools;
    using System.Collections;
    using UnityEngine;

    public class CannonBall : MonoBehaviour
    {
        public CannonBallPool CannonBallPool;
        
        [Header("Visual Setup")]
        [SerializeField] protected SpriteRenderer _renderer;
        [SerializeField] protected ExplosionSpriteChanger _explosion;

        [Header("Attributes")]
        [SerializeField] protected float _speed = 10;
        protected float _currentSpeed;
        [SerializeField] protected int _damage = 20;
        [SerializeField] protected float _timeToBackPool = 1.5f;

        [Header("Audio Setup")]
        [SerializeField] protected AudioClip _firedAudio;
        [SerializeField] protected AudioClip _impactAudio;
        [SerializeField] protected AudioSource _source;

        private Collider2D _collider;


        protected virtual void Awake()
        {
            _currentSpeed = _speed;
            _source.clip = _firedAudio;
            _collider = GetComponent<Collider2D>();
            _source = GetComponent<AudioSource>();
        }

        protected virtual void Update()
        {
            TranslateCannonballFoward();
        }
        
        protected virtual void TranslateCannonballFoward()
        {
            transform.Translate(_currentSpeed * Time.deltaTime * Vector3.up);
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<DamageGateway>(out var damageGateway))
            {
                damageGateway.ApplyDamageOnHealth(_damage);
                DisableVisualAndCollisionComponents();
                _source.PlayOneShot(_impactAudio);
                _explosion.PlaySpriteExplosion();
                _currentSpeed = 0;
            }
        }

        protected void DisableVisualAndCollisionComponents()
        {
            _collider.enabled = false;
            _renderer.enabled = false;
        }

        protected virtual void OnEnable()
        {
            EnableVisualAndColisionComponents();
            ResetSpeedAndAudio();
            StartOnBackToPool();
        }

        protected void EnableVisualAndColisionComponents()
        {
            _renderer.enabled = true;
            _collider.enabled = true;
        }

        protected void ResetSpeedAndAudio()
        {
            _currentSpeed = _speed;
            _source.clip = _firedAudio;
        }

        protected virtual void StartOnBackToPool()
        {
            StartCoroutine(nameof(TimerToBackPool));
        }

        protected virtual IEnumerator TimerToBackPool()
        {
            float count = 0;
            while (count <= _timeToBackPool)
            {
                count += Time.deltaTime;
                yield return null;
            }
            CannonBallPool.BackToPool(this);
        }
    }
}
