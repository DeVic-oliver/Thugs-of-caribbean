using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Core.Components.Counters
{
    public class ScoreCounter : MonoBehaviour
    {
        public static int Score { get; private set; }

        [SerializeField] private List<TextMeshProUGUI> _pointCounterUI;

        private void LateUpdate()
        public static void ResetScore()
        {
            Score = 0;
        }

        public static void IncrementScore()
        {
            foreach (var counter in _pointCounterUI)
            {
                counter.text = $"Score: {PlayerPoints.ToString()}";
            }
            Score++;
        }

        public void ResetScore()
        {
            PlayerPoints = 0;
        }
    }
}