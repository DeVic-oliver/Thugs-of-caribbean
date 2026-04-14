using System.Collections;
using UnityEngine;

namespace TOC.Core.SpawnSystem
{
    public class SpawnSpot : MonoBehaviour
    {
        [SerializeField] private SpawnSpotParameters m_Parameters;
        private Coroutine m_Interval;

        void Update()
        {
            m_Interval ??= StartCoroutine(nameof(OnWaitInterval));
        }

        private void InstantiatePrefabs()
        {
            int n = m_Parameters.Quantity;
            while (n > 0) 
            {
                Instantiate(m_Parameters.Prefab, transform, true);
                n--;
            }

            StopAllCoroutines();
            m_Interval = null;
        }

        private IEnumerator OnWaitInterval()
        {
            yield return new WaitForSeconds(m_Parameters.IntervalDuration);
            InstantiatePrefabs();
        }
    }
}