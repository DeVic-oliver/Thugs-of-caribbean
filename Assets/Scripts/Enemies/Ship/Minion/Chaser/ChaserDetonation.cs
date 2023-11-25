namespace Assets.Scripts.Enemies.Ship.Minion.Chaser
{
    using Assets.Scripts.Core.Components.Damage;
    using UnityEngine;
    using UnityEngine.Events;

    public class ChaserDetonation : MonoBehaviour
    {
        public UnityEvent OnDetonation;

        [SerializeField] private float _damage = 25f;

        private readonly string _detonationTag = "Player";


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.CompareTag(_detonationTag))
            {
                OnDetonation?.Invoke();
                DamageGateway damageGateway = collision.gameObject.GetComponent<DamageGateway>();
                damageGateway.ApplyDamageOnHealth( _damage );
            }
        }
    }
}