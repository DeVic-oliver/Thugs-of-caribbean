using System.Collections;
using UnityEngine;
using Devic.Scripts.Utils.JumpRaycaster;
using Assets.Scripts.Core.Interfaces;

namespace Assets.Scripts.Player
{
    public class PlayerMovment : MonoBehaviour, IMoveable
    {
        public bool IsMoving { get; private set; }
        public bool HasJumped { get; private set; }
        
        private CharacterController _controller;
        
        private bool _isPlayerGrounded;
        private float _gravity = -9.8f;
        private float _fallForce = 3f;
        [SerializeField] private float _playerMoveSpeed = 10f;
        [SerializeField] private float _jumpThrust = 8f;
        private Vector3 _jumpVelocity;

        private Collider _playerCollider;

        

        // Use this for initialization
        void Start()
        {
            _controller = GetComponent<CharacterController>();
            _playerCollider = GetComponent<Collider>();
        }

        // Update is called once per frame
        void Update()
        {
            Move(true);
        }
        public void Move(bool isAlive)
        {
            if (isAlive)
            {
                _isPlayerGrounded = JumpRaycaster.CheckIfIsGrounded(_playerCollider);
                MoveByCharacterController();
            }
        }
        private void MoveByCharacterController() 
        {
            SetPlayerYVelocityToZero();
            MovePlayerByAxis();
            WatchForPlayerJump();
        }
        private void SetPlayerYVelocityToZero()
        {
            if (_isPlayerGrounded && _jumpVelocity.y < 0)
            {
                _jumpVelocity.y = 0f;
                HasJumped = false;
            }
        }
        private void MovePlayerByAxis()
        {
            var move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            _controller.Move(move * Time.deltaTime * _playerMoveSpeed);
            

            if (move != Vector3.zero)
            {
                IsMoving = true;
                gameObject.transform.forward = move;
            }
            else
            {
                IsMoving = false;
            }
        }
        private void WatchForPlayerJump()
        {
            if (Input.GetButtonDown("Jump") && _isPlayerGrounded)
            {   
                HasJumped = true;
                _jumpVelocity.y += Mathf.Sqrt(-_jumpThrust * _fallForce * _gravity);
            }

            ReturnPlayerToGround();
        }
        private void ReturnPlayerToGround()
        {
            _jumpVelocity.y += _gravity * Time.deltaTime;
            _controller.Move(_jumpVelocity * Time.deltaTime);
        }
    }
}