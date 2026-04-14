using TOC.Core.EntitySystem;
using UnityEngine;

namespace TOC.Core.LootSystem
{
    [CreateAssetMenu(fileName = "Reward_Health_", menuName = "TOC/LootSystem/Reward/Health")]
    public class RewardHealthParameters : RewardParameters
    {
        public override void GiveReward(Entity value)
        {
            if (m_Amount > 0)
                value.AddHealth(m_Amount);
            else if (m_Amount < 0)
                value.RemoveHealth(m_Amount);
        }
    }
}