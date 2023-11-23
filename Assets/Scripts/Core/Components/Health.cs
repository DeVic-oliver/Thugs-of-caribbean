namespace Assets.Scripts.Core.Components
{
    using UnityEngine;
    using UnityEngine.Events;

    public class Health : MonoBehaviour
    {
        public UnityEvent OnDie;

        public bool IsAlive { get; protected set; }
        public bool HasJustDied { get; set; }
        public float CurrentHealth { get; protected set; }

        [SerializeField] protected float _health = 100f;


        public void ResetStatus()
        {
            CurrentHealth = _health;
            IsAlive = true;
            HasJustDied = false;
        }

        public virtual void DecrementHealth()
        {
            CurrentHealth = GetZeroOrPositiveHealthDecreasedBy(1f);
        }

        public virtual void DecreaseHealth(float value)
        {
            CurrentHealth = GetZeroOrPositiveHealthDecreasedBy(value);
        }
        
        protected float GetZeroOrPositiveHealthDecreasedBy(float value)
        {
            var health = CurrentHealth - value;

            if (health <= 0f)
            {
                OnDie?.Invoke();
                return 0;
            }

            return health;
        }

        public void SetCurrentHealthToZero()
        {
            CurrentHealth = 0;
        }

        public float GetHealthPercentage()
        {
            var percentage = (CurrentHealth * 100f) / GetTotalHealth();
            return percentage;
        }

        public float GetTotalHealth()
        {
            return _health;
        }

        private void Awake()
        {
            CurrentHealth = _health;
            HasJustDied = false;
            IsAlive = CheckIfIsAliveByHealthAmmout();
        }

        protected virtual void Update()
        {
            IsAlive = CheckIfIsAliveByHealthAmmout();
        }
        
        public bool CheckIfIsAliveByHealthAmmout()
        {
            return (CurrentHealth > 0);
        }

    }
}