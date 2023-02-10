using UnityEngine;

namespace Assets.Scripts.Core.Components
{
    public abstract class Health : MonoBehaviour
    {
        public bool IsAlive { get; protected set; }
        public float CurrentHealth { get; protected set; }
        [SerializeField] protected float _health = 100f;

        protected virtual void DecreaseHealth()
        {
            CurrentHealth = GetZeroOrPositiveHealthDecreasedByValue(1f);
        }

        protected virtual void DecreaseHealth(float value)
        {
            CurrentHealth = GetZeroOrPositiveHealthDecreasedByValue(value);
        }

        public float GetTotalHealth()
        {
            return _health;
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

        protected virtual void Start()
        {
            CurrentHealth = _health;
        }

        protected virtual void Update()
        {
            IsAlive = CheckIfIsAliveByHealthAmmout();
        }
        
        protected bool CheckIfIsAliveByHealthAmmout()
        {
            if (CurrentHealth > 0)
            {
                return true;
            }
            return false;
        }

    }
}