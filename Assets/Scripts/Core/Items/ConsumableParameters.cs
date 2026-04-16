using System.Collections.Generic;
using TOC.Core.Interfaces;
using TOC.Core.Items.LootSystem;
using UnityEngine;
using TOC.Core.EntitySystem;

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
        public void Use(Entity entity)
        {
            foreach (var item in m_Rewards)
                item.GiveReward(entity);
        }
        #endregion

        #region Classes
        public class ConsumableData : ItemData
        {
            protected new readonly ConsumableParameters m_Parameters;
            private readonly List<RewardParameters> m_Rewards;

            public ConsumableData(ConsumableParameters parameters) : base(parameters)
            {
                m_Parameters = parameters;
                m_Rewards = new(parameters.m_Rewards);
            }

            public override void Use(Entity value)
            {
                foreach (var item in m_Rewards)
                    item.GiveReward(value);
            }
        } 
        #endregion
    }
}