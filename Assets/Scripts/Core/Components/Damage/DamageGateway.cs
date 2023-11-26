namespace Assets.Scripts.Core.Components.Damage
{
    using UnityEngine;
    using UnityEngine.Events;

    public class DamageGateway : MonoBehaviour
    {
        public UnityEvent OnDamage;
        public Health _health;


        public void ApplyDamageOnHealth(float damage)
        {
            _health.DecreaseHealth(damage);
            OnDamage?.Invoke();
        }
    }
}