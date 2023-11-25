namespace Assets.Scripts.Core.Components.Cannon
{
    using Assets.Scripts.Core.Components.Projectile;
    using System.Collections.Generic;
    using UnityEngine;
    
    public abstract class Cannon : MonoBehaviour
    {
        [Header("Cannon Setup")]
        [SerializeField] protected List<Transform> _cannonsTransform;
        [SerializeField] protected CannonBall _cannonBall;

        public abstract void Shoot();
    }
}