using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player.StateMachine.ConcreteStates
{
    using Devic.Scripts.Utils.StateMachine;

    public class MoveState : IConcreteState
    {
        private Animator _animator;
        private PlayerMovment _playerMovement;

        public MoveState() { }
        
        public MoveState(Animator animator, PlayerMovment playerMovment)
        {
            _animator = animator;
            _playerMovement = playerMovment;
        }

        public void OnStateEnter(StateMachine stateMachine)
        {
            Debug.Log("Entered in Move State");
            _animator.SetBool("move", true);
        }

        public void OnUpdateState(StateMachine stateMachine)
        {
            if(_playerMovement != null && !_playerMovement.IsMoving)
            {
                _animator.SetBool("move", false);
                stateMachine.SwitchState("IDLE");
            }
            else
            {
                //Debug.Log("Running MOVE STATE");
            }
        }
    }
}