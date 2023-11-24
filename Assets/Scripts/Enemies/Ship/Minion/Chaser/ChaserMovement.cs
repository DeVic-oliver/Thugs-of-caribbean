namespace Assets.Scripts.Enemies.Ship.Minion.Chaser
{
    using Assets.Scripts.Core.Enemies;
    using UnityEngine;


    public class ChaserMovement : MonoBehaviour
    {
        [SerializeField] private GameObject _objectToMovement;
        [SerializeField] private TargetNearbyDetector _detector;
        [SerializeField] private float _rangeDetection = 15f;
        [SerializeField] private float _rotationSpeed = 2f;
        [SerializeField] private float _moveSpeed = 4f;

        private bool _shouldPursueTarget;


        public void PreventPursue()
        {
            _shouldPursueTarget = false;
        }

        private void Start()
        {
            AllowPursue();
        }

        public void AllowPursue()
        {
            _shouldPursueTarget = true;
        }

        void Update()
        {
            if (_shouldPursueTarget)
                MoveTowardsEnemy();
        }

        private void MoveTowardsEnemy()
        {
            var direction = GetMoveTowardsEnemyVector();
            GetObjectTransform().position = new Vector3(direction.x, direction.y, 0);
            LookToTargetSmoothly();
        }

        private Vector2 GetMoveTowardsEnemyVector()
        {
            return Vector2.MoveTowards(GetObjectTransform().position, GetTargetTransform().position, _moveSpeed * Time.deltaTime);
        }

        private void LookToTargetSmoothly()
        {
            Vector3 euler = GetEulerVectorForRotation();
            var lookRotation = Quaternion.Euler(euler);
            GetObjectTransform().rotation = Quaternion.Slerp(GetObjectTransform().rotation, lookRotation, _rotationSpeed * Time.deltaTime);
        }

        private Vector3 GetEulerVectorForRotation()
        {
            var direction = (GetTargetTransform().position - GetObjectTransform().position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            float offset = 90f;
            return new Vector3(0, 0, angle - offset);
        }

        private Transform GetObjectTransform()
        {
            return _objectToMovement.transform;
        }

        private Transform GetTargetTransform()
        {
            return _detector.Target.transform;
        }

    }
}