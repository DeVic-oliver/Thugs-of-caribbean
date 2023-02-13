using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Core.Components
{
    public class HealthUIManager : MonoBehaviour
    {
        [SerializeField] protected Image _fillableHealthBar;
        [SerializeField] protected Health _health;


        private void Update()
        {
            UpdateHealthBar();
        }
        private void UpdateHealthBar()
        {
            _fillableHealthBar.fillAmount = GetNormalizedPercentage();
        }
        private float GetNormalizedPercentage()
        {
            var percentage = _health.GetHealthPercentage();
            return percentage / 100f;
        }
    }
}