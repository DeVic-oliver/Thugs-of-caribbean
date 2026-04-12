using System.Collections.Generic;
using UnityEngine;

namespace TOC.Core.CombatSystem
{ 
    public class Cannon : MonoBehaviour
    {
        [SerializeField] private GameObject m_Prefab;
        [SerializeField] private List<Transform> m_SpawnSpots;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                Fire();
        }

        public void Fire()
        {
            foreach (Transform t in m_SpawnSpots)
                Instantiate(m_Prefab, t.position, t.rotation);
        }
    }
}