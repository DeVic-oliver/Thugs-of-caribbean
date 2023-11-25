namespace Assets.Scripts.Core.Components.Counters
{
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    
    public class ScoreCounter : MonoBehaviour
    {
        public static int Score { get; private set; }

        [SerializeField] private List<TextMeshProUGUI> _pointCounterUI;


        public static void ResetScore()
        {
            Score = 0;
        }

        public static void IncrementScore()
        {
            Score++;
        }

        private void Update()
        {
            foreach (TextMeshProUGUI counter in _pointCounterUI)
                counter.text = $"Score: {Score}";
        }

    }
}