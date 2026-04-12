namespace Assets.Scripts.Core.Components.Spawner
{
    using UnityEngine;
    
    public class SpawnArea : MonoBehaviour
    {
        public bool IsInvisible { get; private set; }

        private Renderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }

        private void Start()
        {
            CheckIfIsInvisibleByCamera();
        }
        private void CheckIfIsInvisibleByCamera()
        {
            if (!_renderer.isVisible)
            {
                IsInvisible = true;
            }
        }

        private void OnBecameVisible()
        {
            IsInvisible = false;
        }

        private void OnBecameInvisible()
        {
            IsInvisible = true;
        }

        public void SpawnGameObject(GameObject gameObject)
        {
            var obj = Instantiate(gameObject, transform.position, gameObject.transform.rotation);
            if(obj.transform.parent != null)
            {
                obj.transform.SetParent(null);
            }
        }
    }
}