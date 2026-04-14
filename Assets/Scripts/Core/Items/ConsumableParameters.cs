using System.Collections.Generic;
using TOC.Core.Interfaces;
using TOC.Core.LootSystem;
using UnityEngine;

namespace TOC.Core.Items
{
    [CreateAssetMenu(fileName = "Consumable_", menuName = "TOC/Items/Consumable")]
    public class ConsumableParameters : ItemParameters, IParameters<ConsumableParameters.ConsumableData>
    {
        #region Fields
        [SerializeField] public List<RewardParameters> m_Rewards = new();
        #endregion

        #region Public Methods
        public ConsumableData GetData() => new(this);
        #endregion

        #region Classes
        public class ConsumableData : ItemData
        {
            protected new readonly ConsumableParameters m_Parameters;

            public ConsumableData(ConsumableParameters parameters) : base(parameters)
            {
                m_Parameters = parameters;
            }
        } 
        #endregion
    }
}