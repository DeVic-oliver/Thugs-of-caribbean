using System.Collections;
using UnityEngine;
using TMPro;

namespace Assets.Scripts.Core.Components.Counters
{
    public class TimerCounter : MonoBehaviour
    {
        public bool HasTimerReachedZero { get; private set; }
        private float _timer;
        [SerializeField] private TextMeshProUGUI _timerUI;

        public void StartTimer()
        {
            var limit = PlayerPrefs.GetInt("TIMER_LIMIT");
            _timer = limit * 60;
            HasTimerReachedZero = false;
            StartCoroutine("StartCounter");
        }
        private IEnumerator StartCounter()
        {
            while(_timer >= 0)
            {
                _timer -= Time.deltaTime;
                _timerUI.text = DisplayTime(_timer);
                yield return new WaitForSeconds(Time.deltaTime);
            }
            HasTimerReachedZero = true;
        }
        private string DisplayTime(float timeToDisplay)
        {
            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);
            return string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}