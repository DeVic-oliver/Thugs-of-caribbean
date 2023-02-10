using UnityEngine;

namespace Assets.Scripts.Core.Enemies
{
    public abstract class MeleeEnemy : EnemyBase
    {
        [SerializeField] protected float _moveSpeed;

        protected virtual void MoveTowardsEnemy()
        {
            var direction = GetMoveTowardsEnemyVector();
            transform.position = new Vector3(direction.x, transform.position.y, direction.z);
            LookToTargetSmoothly();
        }
     
        public Vector3 GetMoveTowardsEnemyVector()
        {
            return Vector3.MoveTowards(transform.position, _enemyGameObject.transform.position, _moveSpeed * Time.deltaTime);
        }
    }
}
