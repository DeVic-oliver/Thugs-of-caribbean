using TOC.Core.EntitySystem;
using System;
using UnityEngine;

namespace TOC.Core.LootSystem
{
    public abstract class RewardParameters : ScriptableObject
    {
        #region Fields
        public int m_Amount;
        #endregion

        #region Properties
        public int Amount => m_Amount;
        #endregion

        public abstract void GiveReward(Entity value);
    }
}
