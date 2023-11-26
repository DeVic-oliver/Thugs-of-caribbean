namespace Assets.Scripts.Core.Components
{
    using UnityEngine;
    using UnityEngine.UI;
    
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
            float percentage = _health.GetHealthPercentage();
            return percentage / 100f;
        }
    }
}