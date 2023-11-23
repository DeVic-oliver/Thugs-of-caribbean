namespace Assets.Scripts.Enemies.Ship.Minion.Chaser
{
    using Assets.Scripts.Core.Components;

    public class ChaserHealth : Health
    {
        public void LoseAllHealthPoints()
        {
            DecreaseHealth(CurrentHealth);
        }
    }
}