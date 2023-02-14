namespace Assets.Scripts.GameManager.StateMachine
{
    using Devic.Scripts.Utils.StateMachine;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class PauseState : IConcreteState
    {
        private PlayerInput _inputSystem;
        private InputAction _pauseAction;

        public PauseState(PlayerInput inputSystem)
        {
            _inputSystem = inputSystem;
            _pauseAction = _inputSystem.actions.FindAction("Pause");
        }

        public void OnStateEnter(StateMachine stateMachine)
        {
            PauseGame();
        }
        private void PauseGame()
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }
        }
        public void OnUpdateState(StateMachine stateMachine)
        {
            ChangeToPauseStateWhenPauseActionIsPerformed(stateMachine);

        }
        private void ChangeToPauseStateWhenPauseActionIsPerformed(StateMachine stateMachine)
        {
            _pauseAction.performed += ctx => stateMachine.SwitchState("GAMEPLAY");
        }
    }
}