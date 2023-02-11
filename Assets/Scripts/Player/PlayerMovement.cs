using UnityEngine;
using Assets.Scripts.Core.Interfaces;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(PlayerHealth))]
    public class PlayerMovement : MonoBehaviour, IMoveable
    {
        private PlayerHealth _health;

        [SerializeField] private float _moveSpeed = 250f;
        [SerializeField] private float _rotateSpeed = 130f;

        private Rigidbody2D _rigidbody;

        private float _unitsPerMove = 1f;

        void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _health = GetComponent<PlayerHealth>();
        }

        void Update()
        {
            Move(_health.IsAlive);
        }
        public void Move(bool isAlive)
        {
            if (isAlive)
            {
                SetPlayerRotation();
            }
        }
        private void SetPlayerRotation()
        {
            Vector3 eulers = GetRotationVector();
            transform.Rotate(eulers);
        }
        private Vector3 GetRotationVector()
        {
            float inputValue = GetHorizontalAxisOpositeValue();
            return new Vector3(0, 0, inputValue * Time.deltaTime * _rotateSpeed);
        }
        private float GetHorizontalAxisOpositeValue()
        {
            return -Input.GetAxis("Horizontal");
        }

        private void FixedUpdate()
        {
            MoveRigidbodyIfIsAlive();
        }
        private void MoveRigidbodyIfIsAlive()
        {
            if (_health.IsAlive && Input.GetKey(KeyCode.W))
            {
                
                Vector2 dir = GetDirectionWherePlayerFaces();
                _rigidbody.MovePosition(dir);
            }
        }
        private Vector2 GetDirectionWherePlayerFaces()
        {
              return transform.position + (_unitsPerMove * transform.up * _moveSpeed * Time.deltaTime);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("island"))
            {
                ResetPhysicsVelocity();
            }
        }
        private void ResetPhysicsVelocity()
        {
            _rigidbody.angularVelocity = 0f;
            _rigidbody.velocity = Vector2.zero;
        }

    }
}