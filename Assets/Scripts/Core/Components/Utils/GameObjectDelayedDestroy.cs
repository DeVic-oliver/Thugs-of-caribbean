namespace Assets.Scripts.Core.Components.Utils
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.Events;

    public class GameObjectDelayedDestroyer : MonoBehaviour
    {
        public UnityEvent PreOnDestroyCallbacks;

        [SerializeField] protected float _secondsToDestroy = 1.5f;
        private Coroutine _currentCoroutine;


        public void DestroyGameObjectAfterDelay()
        {
            if (_currentCoroutine == null)
                _currentCoroutine = StartCoroutine(nameof(DestroyInstance));
        }

        private IEnumerator DestroyInstance()
        {
            PreOnDestroyCallbacks?.Invoke();
            yield return new WaitForSeconds(_secondsToDestroy);
            Destroy(gameObject);
        }

    }
}