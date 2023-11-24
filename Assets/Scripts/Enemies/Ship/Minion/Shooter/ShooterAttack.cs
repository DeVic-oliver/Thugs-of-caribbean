namespace Assets.Scripts.Enemies.Ship.Minion.Shooter
{
    using Assets.Scripts.Core.Enemies;
    using Assets.Scripts.Utils.SightRaycast._2D;
    using UnityEngine;


    [RequireComponent(typeof(ShooterCannon))]
    public class ShooterAttack : MonoBehaviour
    {
        [SerializeField] private ShooterCannon _shooterCannon;
        [SerializeField] private bool _isEnemyOnSight;
        [SerializeField] private TargetNearbyDetector _targetNearbyDetector;

        private readonly string _targetLayerMask = "Player";

        private bool _canAttack;


        public void AllowAttack()
        {
            _canAttack = true;
        }

        public void PreventAttack() 
        {
            _canAttack = false;
        }

        private void Awake()
        {
            _shooterCannon = GetComponent<ShooterCannon>(); 
        }

        private void Update()
        {
            _isEnemyOnSight = SightRaycaster2D.CheckGameObjectOnSight(transform, _targetNearbyDetector.GetRangeDetection(), _targetLayerMask);

            if (_canAttack && _isEnemyOnSight)
                _shooterCannon.Shoot();
        }
    }
}