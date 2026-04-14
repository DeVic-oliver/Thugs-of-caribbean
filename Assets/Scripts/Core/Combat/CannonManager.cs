using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TOC.Core.CombatSystem
{ 
    public class CannonManager : MonoBehaviour
    {
        #region Fields
        [SerializeField] private List<Cannon> m_Cannons;
        [SerializeField, MinValue(0f)] private float m_ShootDelay = 0.5f;
        
        private Coroutine m_Delay;
        #endregion

        #region Unity Methods
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                m_Delay ??= StartCoroutine(OnDelayShoot(true));
            else if (Input.GetKeyDown(KeyCode.Mouse1))
                m_Delay ??= StartCoroutine(OnDelayShoot(false));

        }
        #endregion

        #region Private Methods
        private void Fire(bool leftSide = true)
        {
            foreach (var item in m_Cannons)
            {
                if((leftSide && !item.LeftSide) || (!leftSide && item.LeftSide)) 
                    continue;

                item.Fire();
            }
        }
        #endregion

        #region Events
        private IEnumerator OnDelayShoot(bool leftSide)
        {
            yield return new WaitForSeconds(m_ShootDelay);
            Fire(leftSide);
            m_Delay = null;
        }
        #endregion
    }
}