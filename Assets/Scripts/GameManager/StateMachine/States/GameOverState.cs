namespace Assets.Scripts.GameManager.StateMachine
{ 
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;
    using Assets.Scripts.Core.Components.Audio;

    public class GameOverState : GameplayConcreteState
    {
        private GameObject _gameOverUI;
        private Button _restartButton;
        private Button _exitButton;
        private bool _canRestartGame = false;

        private int _mainMenuSceneIndex = 0;

        private UIAudioManagerBase _uiAudioManager;

        public GameOverState(GameObject gameoverUI, Button restartButton, Button exitButton, UIAudioManagerBase uiAudioManager)
        {
            _gameOverUI = gameoverUI;
            _restartButton = restartButton;
            _exitButton = exitButton;

            _restartButton.onClick.AddListener(RestartGame);
            _exitButton.onClick.AddListener(ExitGame);
            _uiAudioManager = uiAudioManager;

        }
        private void RestartGame()
        {
            _uiAudioManager.PlayButtonClick();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        private void ExitGame()
        {
            _uiAudioManager.PlayButtonClick();
            Time.timeScale = 1;
            SceneManager.LoadScene(_mainMenuSceneIndex);
        }

        public override void OnStateEnter(GameplayStateMachine GameplayStateMachine)
        {
            Time.timeScale = 0;
            _canRestartGame = false;
            _gameOverUI.SetActive(true);
        }

        public override void OnUpdateState(GameplayStateMachine GameplayStateMachine)
        {
            CheckIfCanGoToStartState(GameplayStateMachine);
        }
        private void CheckIfCanGoToStartState(GameplayStateMachine GameplayStateMachine)
        {
            if (_canRestartGame)
            {
                _gameOverUI.SetActive(false);
                //GameplayStateMachine.SwitchState("START");
            }
        }
    }
}