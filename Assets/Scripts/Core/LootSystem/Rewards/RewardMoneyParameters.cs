using TOC.Core.EntitySystem;
using UnityEngine;

namespace TOC.Core.LootSystem
{
    [CreateAssetMenu(fileName = "Reward_Money_", menuName = "TOC/LootSystem/Reward/Money")]
    public class RewardMoneyParameters : RewardParameters
    {
        public override void GiveReward(Entity value)
        {
            value.Money += m_Amount;
        }
    }
}