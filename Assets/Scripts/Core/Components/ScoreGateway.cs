namespace Assets.Scripts.Core.Components
{
    using Assets.Scripts.Core.Components.Counters;
    using UnityEngine;

    public class ScoreGateway : MonoBehaviour
    {
        public void IncrementScore()
        {
            ScoreCounter.IncrementScore();
        }
    }
}