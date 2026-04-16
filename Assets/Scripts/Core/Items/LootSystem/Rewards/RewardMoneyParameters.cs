using TOC.Core.EntitySystem;
using UnityEngine;

namespace TOC.Core.Items.LootSystem
{
    [CreateAssetMenu(fileName = "Reward_Money_", menuName = "TOC/Items/LootSystem/Reward/Money")]
    public class RewardMoneyParameters : RewardParameters
    {
        public override void GiveReward(Entity value)
        {
            value.Money += m_Amount;
        }
    }
}