using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Core.Components.Utils
{
    public abstract class GameObjectDisabler : MonoBehaviour
    {
        [SerializeField] protected float _timeToDisable = 1.5f;

        protected virtual void DisableMe()
        {
            StartCoroutine("DisableInstance");
        }
        protected virtual IEnumerator DisableInstance()
        {
            float count = 0;
            while (count < _timeToDisable)
            {
                count += Time.deltaTime;
                yield return null;
            }
            gameObject.SetActive(false);
        }
    }
}