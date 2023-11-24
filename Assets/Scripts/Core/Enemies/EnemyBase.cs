namespace Assets.Scripts.Core.Enemies
{
    using System.Collections;
    using UnityEngine;
    using Assets.Scripts.Core.Components.Damage;
    using Assets.Scripts.Core.Components;

    [RequireComponent(typeof(DamageComponent), typeof(Health))]
    public abstract class EnemyBase : MonoBehaviour
    {
        [SerializeField] protected int _enemyScoreValue;

        protected Health _health;
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
            _health = GetComponent<Health>();
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
            if (!_health.IsAlive && !_health.HasJustDied)
            {
                _health.HasJustDied = true;
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

        protected void DecreaseHealthByDamageWithFlashFeedback(int damage)
        {
            _damageComponent.FlashShader();
            _health.DecreaseHealth(damage);
        }
    }
}