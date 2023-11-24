namespace Assets.Scripts.Core.Components.Damage
{
    using UnityEngine;

    public class DamageGateway : MonoBehaviour
    {
        public Health _health;


        public void ApplyDamageOnHealth(float damage)
        {
            _health.DecreaseHealth(damage);
        }
    }
}