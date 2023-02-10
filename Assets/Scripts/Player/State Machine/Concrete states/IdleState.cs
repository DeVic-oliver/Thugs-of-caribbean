using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player.StateMachine.ConcreteStates
{
    using Devic.Scripts.Utils.StateMachine;

    public class IdleState : IConcreteState
    {
        private Animator _animator;
        private PlayerMovment _playerMovement;
        private GameObject _gameObject;

        public IdleState() { }

        public IdleState(Animator animator, PlayerMovment playerMovment) 
        {
            _animator = animator;
            _playerMovement = playerMovment;
        }


        public void OnStateEnter(StateMachine stateMachine)
        {
            Debug.Log("Entered in IDLE state");
            _animator.SetTrigger("idle");
        }

        public void OnUpdateState(StateMachine stateMachine)
        {
            if(_playerMovement != null && _playerMovement.IsMoving )
            {
                stateMachine.SwitchState("MOVE");
            }
            else
            {
                //Debug.Log("Running IDLE STATE");
            }
        }
    }
}