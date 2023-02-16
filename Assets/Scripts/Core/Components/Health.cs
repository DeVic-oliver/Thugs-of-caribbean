using UnityEngine;

namespace Assets.Scripts.Core.Components
{
    public abstract class Health : MonoBehaviour
    {
        public bool IsAlive { get; protected set; }
        public bool HasDied { get; set; }
        public float CurrentHealth { get; protected set; }
        [SerializeField] protected float _health = 100f;

        public virtual void DecreaseHealth()
        {
            CurrentHealth = GetZeroOrPositiveHealthDecreasedByValue(1f);
        }
        public virtual void DecreaseHealth(float value)
        {
            CurrentHealth = GetZeroOrPositiveHealthDecreasedByValue(value);
        }

        public float GetTotalHealth()
        {
            return _health;
        }

        public void KillMe()
        {
            CurrentHealth = 0;
        }

        public float GetHealthPercentage()
        {
            var percentage = (CurrentHealth * 100f) / GetTotalHealth();
            return percentage;
        }

        protected float GetZeroOrPositiveHealthDecreasedByValue(float value)
        {
            var health = CurrentHealth - value;
            if(health < 0)
            {
                return 0;
            }
            return health;
        }

        private void Awake()
        {
            CurrentHealth = _health;
            HasDied = false;
            IsAlive = CheckIfIsAliveByHealthAmmout();
        }

        protected virtual void Update()
        {
            IsAlive = CheckIfIsAliveByHealthAmmout();
        }
        
        public bool CheckIfIsAliveByHealthAmmout()
        {
            if (CurrentHealth > 0)
            {
                return true;
            }
            return false;
        }

    }
}