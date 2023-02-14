namespace Assets.Scripts.GameManager.StateMachine
{ 
    using Devic.Scripts.Utils.StateMachine;
    using UnityEngine;
    public class GameOverState : IConcreteState
    {
        public void OnStateEnter(StateMachine stateMachine)
        {
            Debug.Log("WELCOME TO GAME OVER!!");
        }

        public void OnUpdateState(StateMachine stateMachine)
        {
            Debug.Log("WELCOME TO GAME OVER!!");
        }
    }
}