using UnityEngine;
using Assets.Scripts.Core.Interfaces;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(PlayerHealth))]
    public class PlayerMovement : MonoBehaviour, IMoveable
    {
        private PlayerHealth _health;

        [SerializeField] private float _moveSpeed = 6;
        [SerializeField] private float _rotateSpeed = 2f;

        private Rigidbody2D _rigidbody;
        private float _rotationAngleOffset = 90f;

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
                RotatePlayerByMousePosition();
            }
        }
        private void RotatePlayerByMousePosition()
        {
            float angle = GetRotateAngle();
            transform.rotation = GetSlerpedQuaternionOfPlayerToMousePosition(angle);
        }
        private float GetRotateAngle()
        {
            Vector2 diffVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            diffVector.Normalize();
            return Mathf.Atan2(diffVector.y, diffVector.x) * Mathf.Rad2Deg;
        }
        private Quaternion GetSlerpedQuaternionOfPlayerToMousePosition(float angle) 
        {
            Quaternion rotation = Quaternion.Euler(0, 0, angle - _rotationAngleOffset);
            return Quaternion.Slerp(transform.rotation, rotation, 1f * Time.deltaTime * _rotateSpeed);
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
              var value = Mathf.Abs(Input.GetAxis("Vertical"));
              return transform.position + (value * transform.up * _moveSpeed * Time.deltaTime);
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