using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Core.Components.Counters
{
    public class ScoreCounter : MonoBehaviour
    {
        public static int PlayerPoints;

        [SerializeField] private TextMeshProUGUI _pointCounterUI;

        private void LateUpdate()
        {
            _pointCounterUI.text = $"Score: {PlayerPoints.ToString()}";
        }

        public void ResetScore()
        {
            PlayerPoints = 0;
        }
    }
}