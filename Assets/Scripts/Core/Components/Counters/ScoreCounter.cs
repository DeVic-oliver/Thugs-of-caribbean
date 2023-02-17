using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Core.Components.Counters
{
    public class ScoreCounter : MonoBehaviour
    {
        public static int PlayerPoints;

        [SerializeField] private List<TextMeshProUGUI> _pointCounterUI;

        private void LateUpdate()
        {
            foreach (var counter in _pointCounterUI)
            {
                counter.text = $"Score: {PlayerPoints.ToString()}";
            }
        }

        public void ResetScore()
        {
            PlayerPoints = 0;
        }
    }
}