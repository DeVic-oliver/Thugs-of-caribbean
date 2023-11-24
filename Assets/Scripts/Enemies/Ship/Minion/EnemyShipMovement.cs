namespace Assets.Scripts.Enemies.Ship.Minion
{
    using Assets.Scripts.Core.Enemies;
    using UnityEngine;

    [RequireComponent(typeof(EnemyShipRotation))]
    public class EnemyShipMovement : MonoBehaviour
    {
        [SerializeField] protected GameObject _objectToMovement;
        [SerializeField] protected TargetNearbyDetector _detector;
        [SerializeField] protected EnemyShipRotation _shipRotation;

        protected bool _shouldPursueTarget;


        public void PreventPursue()
        {
            _shouldPursueTarget = false;
        }

        public void AllowPursue()
        {
            _shouldPursueTarget = true;
        }

        protected Transform GetObjectTransform()
        {
            return _objectToMovement.transform;
        }

        protected Transform GetTargetTransform()
        {
            return _detector.Target.transform;
        }

        protected void Awake()
        {
            _shipRotation = GetComponent<EnemyShipRotation>();
        }
    }
}