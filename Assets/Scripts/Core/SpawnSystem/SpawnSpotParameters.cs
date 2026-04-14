using NaughtyAttributes;
using UnityEngine;

namespace TOC.Core.SpawnSystem
{
    [CreateAssetMenu(fileName = "SpawnSpot_", menuName = "TOC/Spawn/Spot")]
    public class SpawnSpotParameters : ScriptableObject
    {
        #region Fields
        [SerializeField] private GameObject m_Prefab;
        [SerializeField, MinValue(1)] private int m_Quantity = 1;
        [SerializeField, MinValue(5f)] private float m_IntervalDuration = 5f;
        #endregion

        #region Properties
        public GameObject Prefab => m_Prefab;
        public int Quantity => m_Quantity;
        public float IntervalDuration => m_IntervalDuration;
        #endregion
    }
}