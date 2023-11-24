namespace Assets.Scripts.Enemies.Ship.Minion.Shooter
{
    using Assets.Scripts.Core.Components.Weapon;
    using Assets.Scripts.Core.Enemies;
    using Assets.Scripts.Utils.SightRaycast._2D;
    using UnityEngine;
    

    public class ShooterAttack : MonoBehaviour
    {
        [SerializeField] private RangedWeapon _weapon;
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

        private void Update()
        {
            _isEnemyOnSight = SightRaycaster2D.CheckGameObjectOnSight(transform, _targetNearbyDetector.GetRangeDetection(), _targetLayerMask);

            if (_canAttack && _isEnemyOnSight)
                _weapon.Shoot();
        }
    }
}