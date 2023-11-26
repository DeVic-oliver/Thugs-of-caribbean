namespace Assets.Scripts.Player
{
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class PlayerMovement : MonoBehaviour
    {
        public bool CanMove;

        [SerializeField] private GameObject _objectToMove;
        [SerializeField] private float _moveSpeed = 6;
        [SerializeField] private float _rotateSpeed = 2f;
        [SerializeField] private Rigidbody2D _rigidbody;
        
        private float _rotationAngleOffset = 90f;
        private bool _isMoving;

        private readonly string _obstacleTag = "island";


        public void MovePlayer(InputAction.CallbackContext context)
        {
            _isMoving = context.performed;
        }

        void Start()
        {
            CanMove = true;
        }

        void Update()
        {
            if (CanMove)
                RotatePlayerByMousePosition();
        }

        private void RotatePlayerByMousePosition()
        {
            float angle = GetRotationAngle();
            _objectToMove.transform.rotation = GetSlerpedQuaternionOfPlayerToMousePosition(angle);
        }

        private float GetRotationAngle()
        {
            Vector2 diffVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            diffVector.Normalize();
            return Mathf.Atan2(diffVector.y, diffVector.x) * Mathf.Rad2Deg;
        }

        private Quaternion GetSlerpedQuaternionOfPlayerToMousePosition(float angle) 
        {
            Quaternion rotation = Quaternion.Euler(0, 0, angle - _rotationAngleOffset);
            return Quaternion.Slerp(transform.rotation, rotation, GetClampedRange());
        }

        private float GetClampedRange()
        {
            return (1f * Time.deltaTime * _rotateSpeed);
        }

        private void FixedUpdate()
        {
            if (CanMove && _isMoving)
                MoveRigidbodyPosition();
        }

        private void MoveRigidbodyPosition()
        {
            Vector2 dir = GetDirectionWherePlayerFaces();
            _rigidbody.MovePosition(dir);
        }

        private Vector2 GetDirectionWherePlayerFaces()
        {
              return _objectToMove.transform.position + (_objectToMove.transform.up * _moveSpeed * Time.deltaTime);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(_obstacleTag))
                ResetPhysicsVelocity();
        }

        private void ResetPhysicsVelocity()
        {
            _rigidbody.angularVelocity = 0f;
            _rigidbody.velocity = Vector2.zero;
        }

    }
}