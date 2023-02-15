﻿namespace Assets.Scripts.GameManager.MainMenu
{
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;
    using TMPro;
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private int _initialLevelIndex;
        [SerializeField] private Button _playButton;
        
        [Space(10f)]
        [Header("Settings setup")]
        [Space(5f)]
        [SerializeField] private Button _settingsButton;
        [SerializeField] private GameObject _settingsUI;
        [SerializeField] private Button _saveSettingsButton;
        [Space(5f)]
        [Header("Sliders")]
        [SerializeField] private Slider _enemySpawnIntervalSlider;
        [SerializeField] private TextMeshProUGUI _enemySpawnCounterUI;
        [Space(5f)]
        [SerializeField] private Slider _gameTimeLimitSlider;
        [SerializeField] private TextMeshProUGUI _gameTimeLimitCounterUI;

        private void Start()
        {
            _settingsUI.SetActive(false);
            InitEventListenersOnButtons();
        }
        private void InitEventListenersOnButtons()
        {
            _playButton.onClick.AddListener(PlayGame);
            _settingsButton.onClick.AddListener(OpenSettings);
            _saveSettingsButton.onClick.AddListener(SaveSettings);
        }
        private void PlayGame()
        {
            SceneManager.LoadScene(_initialLevelIndex);
        }
        private void SaveSettings()
        {
            PlayerPrefs.SetInt("TIMER_LIMIT", (int)_gameTimeLimitSlider.value);
            PlayerPrefs.SetInt("ENEMEIS_SPAWN_INTERVAL", (int)_enemySpawnIntervalSlider.value);
            _settingsUI.SetActive(false);
        }
        private void OpenSettings()
        {
            _settingsUI.SetActive(true);
        }

        private void Update()
        {
            UpdateSlidersCounters();
        }
        private void UpdateSlidersCounters()
        {
            var spawnInterval = (int)_enemySpawnIntervalSlider.value;
            var timeLimit = (int)_gameTimeLimitSlider.value;
            _enemySpawnCounterUI.text = $"{spawnInterval} seconds";
            _gameTimeLimitCounterUI.text = $"{timeLimit} minutes";
        }
    }
}