using DG.Tweening;
using System.Collections;
using UnityEngine;
using Assets.Scripts.Core.Components.Damage;
using System;

namespace Assets.Scripts.Core.Enemies
{
    [RequireComponent(typeof(DamageComponent), typeof(EnemyHealth))]
    public abstract class EnemyBase : MonoBehaviour
    {
        [SerializeField] protected int _enemyScoreValue;

        protected EnemyHealth _health;
        [Header("Death Setup")]
        [SerializeField] private float _timeToDisapearAfterDeath = 2.5f;
        private float _timeToDeisapear;

        [Space(10f)]
        [Header("Detection setup")]
        public GameObject EnemyGameObject;
        [SerializeField] protected float _rangeDetection = 15f;
        [Space(10f)]
        [SerializeField] protected DamageComponent _damageComponent;


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
            _timeToDeisapear = _timeToDisapearAfterDeath;
            _collider.enabled = true;
        }

        private void LateUpdate()
        {
            PlayDeathVFXIfNotAlive();
        }
        private void PlayDeathVFXIfNotAlive()
        {
            if (!_health.IsAlive && !_health.HasDied)
            {
                _health.HasDied = true;
                StartCoroutine("DeathCoroutine");
            }
        }
        IEnumerator DeathCoroutine()
        {
            _damageComponent.PlayDeathVFX();
            _collider.enabled = false;
            while (_timeToDeisapear >= 0)
            {
                _timeToDeisapear -= Time.deltaTime;
                yield return null;
            }
            gameObject.SetActive(false);
        }

        protected virtual bool CheckIfEnemyIsNearby()
        {
            if(Vector3.Distance(EnemyGameObject.transform.position, gameObject.transform.position) < _rangeDetection)
            {
                return true;
            }
            return false;
        }
        protected void LookToTargetSmoothly()
        {
            var direction = (EnemyGameObject.transform.position - transform.position).normalized;
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
    }
}