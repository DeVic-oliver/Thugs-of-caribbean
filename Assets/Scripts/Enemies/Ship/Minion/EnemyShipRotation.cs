namespace Assets.Scripts.Enemies.Ship.Minion
{
    using UnityEngine;

    public class EnemyShipRotation : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 2;


        public void LookToTargetSmoothly(Transform objTransform, Transform targetTransform)
        {
            Vector3 euler = GetEulerVectorForRotation(objTransform, targetTransform);
            var lookRotation = Quaternion.Euler(euler);
            objTransform.rotation = Quaternion.Slerp(objTransform.rotation, lookRotation, _rotationSpeed * Time.deltaTime);
        }

        private Vector3 GetEulerVectorForRotation(Transform objTransform, Transform targetTransform)
        {
            var direction = (targetTransform.position - objTransform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            float offset = 90f;
            return new Vector3(0, 0, angle - offset);
        }
    }
}