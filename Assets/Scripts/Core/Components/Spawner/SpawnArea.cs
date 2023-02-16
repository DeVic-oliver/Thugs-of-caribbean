using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Core.Components.Spawner
{
    public class SpawnArea : MonoBehaviour
    {
        public bool IsInvisible { get; private set; }


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