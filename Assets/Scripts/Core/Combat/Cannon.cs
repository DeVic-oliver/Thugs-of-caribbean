using System.Collections;
using UnityEngine;

namespace TOC.Core.CombatSystem
{ 
    public class Cannon : MonoBehaviour
    {
        #region Fields
        [SerializeField] private bool m_Available;
        [SerializeField] private bool m_LeftSide;
        [SerializeField] private GameObject m_Prefab;
        [SerializeField] private Transform m_SpawnSpot;

        private Coroutine m_OnFireCannon;
        #endregion

        #region Properties
        public bool Available => m_Available;
        public bool LeftSide => m_LeftSide;
        #endregion

        #region Public Methods
        public void Fire()
        {
            m_OnFireCannon ??= StartCoroutine(nameof(OnFireCannon));
        }
        #endregion

        #region Events
        private IEnumerator OnFireCannon()
        {
            float rnd = Random.Range(0.1f, 0.3f);
            yield return new WaitForSeconds(rnd);
            Instantiate(m_Prefab, m_SpawnSpot.position, m_SpawnSpot.rotation);
            m_OnFireCannon = null;
        } 
        #endregion
    }
}