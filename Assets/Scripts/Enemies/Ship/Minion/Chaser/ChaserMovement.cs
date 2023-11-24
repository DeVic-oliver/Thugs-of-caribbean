namespace Assets.Scripts.Enemies.Ship.Minion.Chaser
{
    using UnityEngine;

    [RequireComponent(typeof(EnemyShipRotation))]
    public class ChaserMovement : EnemyShipMovement
    {
        [SerializeField] private float _moveSpeed = 4f;


        void Update()
        {
            if (_shouldPursueTarget)
                MoveTowardsEnemy();
        }

        private void MoveTowardsEnemy()
        {
            var direction = GetMoveTowardsEnemyVector();
            GetObjectTransform().position = new Vector3(direction.x, direction.y, 0);
            _shipRotation.LookToTargetSmoothly(GetObjectTransform(), GetTargetTransform());
        }

        private Vector2 GetMoveTowardsEnemyVector()
        {
            return Vector2.MoveTowards(GetObjectTransform().position, GetTargetTransform().position, _moveSpeed * Time.deltaTime);
        }

    }
}