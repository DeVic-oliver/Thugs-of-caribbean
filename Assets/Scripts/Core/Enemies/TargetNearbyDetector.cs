namespace Assets.Scripts.Core.Enemies
{
    using UnityEngine;
    using UnityEngine.Events;

    public class TargetNearbyDetector : MonoBehaviour
    {
        public UnityEvent OnTargetNearby;
        public UnityEvent OnTargetNotNearby;
        public GameObject Target;
        
        [SerializeField] private float _rangeDetection = 15; 


        public float GetRangeDetection()
        {
            return _rangeDetection;
        }

        private void Update()
        {
            if (IsTargetNearby())
            {
                OnTargetNearby?.Invoke();
            }
            else
            {
                OnTargetNotNearby?.Invoke();
            }
        }

        private bool IsTargetNearby()
        {
            float distance = Vector3.Distance(transform.position, GetTargetTransform().position);
            return distance < _rangeDetection;
        }
        
        private Transform GetTargetTransform()
        {
            return Target.transform;
        }

    }
}