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
            _fillableHealthBar.fillAmount = GetHealthPercentage();
        }
        private float GetHealthPercentage()
        {
            var percentage = (_health.CurrentHealth * 100f) / _health.GetTotalHealth();
            percentage = GetNormalizedPercentage(percentage);
            return percentage;
        }
        private float GetNormalizedPercentage(float value)
        {
            return value / 100f;
        }
    }
}