namespace Assets.Scripts.Enemies.Ship.Minion.Chaser
{
    using UnityEngine;
    using UnityEngine.Events;

    public class ChaserDetonation : MonoBehaviour
    {
        public UnityEvent OnDetonation;

        private readonly string _detonationTag = "Player";


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.CompareTag(_detonationTag))
            {
                OnDetonation?.Invoke();
            }
        }
    }
}