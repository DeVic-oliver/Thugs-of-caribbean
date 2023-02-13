using DG.Tweening;
using System.Collections;
using UnityEngine;
using Assets.Scripts.Core.Components.Damage;

namespace Assets.Scripts.Core.Enemies
{
    [RequireComponent(typeof(DamageComponent), typeof(EnemyHealth))]
    public abstract class EnemyBase : MonoBehaviour
    {
        public bool IsAttacking { get; private set; }
        protected EnemyHealth _health;
        
        [Header("Detection setup")]
        [SerializeField] protected float _rangeDetection = 15f;
        [SerializeField] protected GameObject _enemyGameObject;
        [Space(10f)]
        [SerializeField] protected DamageComponent _damageComponent;

        private Coroutine _deathCoroutine;
        private Collider2D _collider;

        public float _rotationSlerpStep = 2f;

        private void Awake()
        {
            _damageComponent = GetComponent<DamageComponent>();
            _collider = GetComponent<Collider2D>();
            _health = GetComponent<EnemyHealth>();
        }

        protected virtual void Start()
        {
            _deathCoroutine = null;
            _collider.enabled = true;
        }

        protected virtual void Update()
        {
            PlayDeathVFXIfNotAlive();
        }
        private void PlayDeathVFXIfNotAlive()
        {
            if (!_health.IsAlive && _deathCoroutine == null)
            {
                _deathCoroutine = StartCoroutine(DeathCoroutine());
                _collider.enabled = false;
            }
        }
        IEnumerator DeathCoroutine()
        {
            var deathVFX = _damageComponent.GetVFXFromDict("death");
            deathVFX.Play();
            while (deathVFX.isPlaying)
            {
                yield return null;
            }
            gameObject.SetActive(false);
        }
        protected virtual bool CheckIfEnemyIsNearby()
        {
            if(Vector3.Distance(_enemyGameObject.transform.position, gameObject.transform.position) < _rangeDetection)
            {
                return true;
            }
            return false;
        }
        protected void LookToTargetSmoothly()
        {
            var direction = (_enemyGameObject.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            float offset = 90f;
            var lookRotation = Quaternion.Euler(new Vector3(0, 0, angle - offset));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, _rotationSlerpStep * Time.deltaTime);
        }

        protected void DecreaseHealthByDamageWithFlashFeedback(int damage)
        {
            _damageComponent.FlashShader();
            _health.DecreaseHealth(damage);
        }

        protected void PlayDamageComponentVFX(string id)
        {
            _damageComponent.GetVFXFromDict(id).Play();
        }
    }
}