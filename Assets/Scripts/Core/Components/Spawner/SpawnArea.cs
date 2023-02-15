using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Core.Components.Spawner
{
    public class SpawnArea : MonoBehaviour
    {
        public bool IsInvisible { get; private set; }

        private void OnBecameInvisible()
        {
            IsInvisible = true;
        }

        private void OnBecameVisible()
        {
            IsInvisible = false;
        }

        public void SpawnGameObject(GameObject gameObject)
        {
            Debug.Log("SPAWNED AT:" + transform.position);
            var obj = Instantiate(gameObject, gameObject.transform);
            obj.transform.SetParent(null);
        }
    }
}