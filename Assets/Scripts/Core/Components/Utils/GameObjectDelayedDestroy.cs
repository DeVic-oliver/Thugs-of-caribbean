namespace Assets.Scripts.Core.Components.Utils
{
    using System.Collections;
    using UnityEngine;
    
    public class GameObjectDelayedDestroyer : MonoBehaviour
    {
        [SerializeField] protected float _secondsToDestroy = 1.5f;

        public void DestroyGameObjectAfterDelay()
        {
            StartCoroutine(nameof(DestroyInstance));
        }

        private IEnumerator DestroyInstance()
        {
            yield return new WaitForSeconds(_secondsToDestroy);
            Destroy(gameObject);
        }
    }
}