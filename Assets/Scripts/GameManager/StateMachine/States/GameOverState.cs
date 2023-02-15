namespace Assets.Scripts.GameManager.StateMachine
{ 
    using Devic.Scripts.Utils.StateMachine;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;

    public class GameOverState : IConcreteState
    {
        private GameObject _gameOverUI;
        private Button _restartButton;
        private Button _exitButton;

        private bool _canRestartGame = false;

        public GameOverState(GameObject gameoverUI, Button restartButton, Button exitButton)
        {
            _gameOverUI = gameoverUI;
            _restartButton = restartButton;
            _exitButton = exitButton;

            _restartButton.onClick.AddListener(RestartGame);
            _exitButton.onClick.AddListener(ExitGame);
        }
        private void RestartGame()
        {
            _canRestartGame = true;
        }
        private void ExitGame()
        {
            Debug.Log("EXITING");

            ///LOAD MAIN MENU SCENE
            //var mainMenu = SceneManager.GetSceneByBuildIndex(0);
            //SceneManager.LoadScene(mainMenu.buildIndex);
        }

        public void OnStateEnter(StateMachine stateMachine)
        {
            Debug.Log("WELCOME TO GAME OVER!!");
            Time.timeScale = 0;
            _canRestartGame = false;
            _gameOverUI.SetActive(true);
        }

        public void OnUpdateState(StateMachine stateMachine)
        {
            CheckIfCanGoToStartState();
        }
        private void CheckIfCanGoToStartState()
        {
            if (_canRestartGame)
            {
                var currentScene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(currentScene.buildIndex);
            }
        }
    }
}