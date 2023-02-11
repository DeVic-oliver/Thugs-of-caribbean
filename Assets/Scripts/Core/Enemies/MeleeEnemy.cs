using UnityEngine;

namespace Assets.Scripts.Core.Enemies
{
    public abstract class MeleeEnemy : EnemyBase
    {
        [SerializeField] protected float _moveSpeed;

        protected virtual void MoveTowardsEnemy()
        {
            var direction = GetMoveTowardsEnemyVector();
            transform.position = new Vector3(direction.x, direction.y, 0);
            LookToTargetSmoothly();
        }
     
        public Vector2 GetMoveTowardsEnemyVector()
        {
            return Vector2.MoveTowards(transform.position, _enemyGameObject.transform.position, _moveSpeed * Time.deltaTime);
        }
    }
}
