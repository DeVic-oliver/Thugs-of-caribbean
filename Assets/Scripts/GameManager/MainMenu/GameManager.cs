namespace Assets.Scripts.GameManager.MainMenu
{
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;
    using TMPro;
    using Assets.Scripts.Core.Components.Audio;
    using System.Collections;

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
        [Space(5f)]
        [Header("Audio")]
        [SerializeField] private UIAudioManagerBase _uiAudioManager;


        private void Awake()
        {
            _uiAudioManager = GetComponent<UIAudioManagerBase>();
        }

        private void Start()
        {
            _settingsUI.SetActive(false);
            InitEventListenersOnButtons();
            InitSliderInvoke();
        }
        private void InitEventListenersOnButtons()
        {
            _playButton.onClick.AddListener(PlayGame);
            _settingsButton.onClick.AddListener(OpenSettings);
            _saveSettingsButton.onClick.AddListener(SaveSettings);
        }
        private void InitSliderInvoke()
        {
            _enemySpawnIntervalSlider.onValueChanged.AddListener(delegate { _uiAudioManager.PlaySliderSound(); });
            _gameTimeLimitSlider.onValueChanged.AddListener(delegate { _uiAudioManager.PlaySliderSound(); });
        }

        private void PlayGame()
        {
            CheckIfSettingsWasSaved();
            StartCoroutine("LoadGameAfterClickSound");
        }
        private IEnumerator LoadGameAfterClickSound()
        {
            _uiAudioManager.PlayButtonClick();
            yield return new WaitForSeconds(_uiAudioManager.ButtonClick.length);
            SceneManager.LoadScene(_initialLevelIndex);

        }
        private void CheckIfSettingsWasSaved()
        {
            if(PlayerPrefs.GetInt("TIMER_LIMIT") == 0 || PlayerPrefs.GetInt("ENEMEIS_SPAWN_INTERVAL") == 0 )
            {
                SavePlayerPrefs();
            }
        }
        private void SaveSettings()
        {
            SavePlayerPrefs();
            _uiAudioManager.PlayButtonClick();
            _settingsUI.SetActive(false);
        }
        private void SavePlayerPrefs()
        {
            PlayerPrefs.SetInt("TIMER_LIMIT", (int)_gameTimeLimitSlider.value);
            PlayerPrefs.SetInt("ENEMEIS_SPAWN_INTERVAL", (int)_enemySpawnIntervalSlider.value);
        }
        private void OpenSettings()
        {
            _uiAudioManager.PlayButtonClick();
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