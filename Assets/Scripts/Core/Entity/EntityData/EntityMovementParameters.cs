using TOC.Core.Interfaces;
using UnityEngine;

namespace TOC.Core.EntitySystem.Data
{
    [CreateAssetMenu(fileName = "EntityMovement_Parameters_", menuName = "TOC/Entity/Movements", order = 0)]
    public class EntityMovementParameters : ScriptableObject, IParameters<EntityMovementParameters.EntityMovementData>
    {
        #region Fields
        [SerializeField] private float m_MovementSpeed = 1f;
        [SerializeField] private float m_RotationSpeed = 1f;
        #endregion

        #region Public Methods
        public EntityMovementData GetData() => new(this);
        #endregion

        #region Classes
        public class EntityMovementData
        {
            #region Fields
            private float m_MovementSpeed;
            private float m_RotationSpeed;
            #endregion

            #region Properties
            public float MovementSpeed => m_MovementSpeed;
            public float RotationSpeed => m_RotationSpeed;
            #endregion

            #region Constructors
            public EntityMovementData(EntityMovementParameters parameters)
            {
                m_MovementSpeed = parameters.m_MovementSpeed;
                m_RotationSpeed = parameters.m_RotationSpeed;
            }

            public EntityMovementData(float movementSpeed, float rotationSpeed)
            {
                m_MovementSpeed = movementSpeed;
                m_RotationSpeed = rotationSpeed;
            }
            #endregion
        }
        #endregion
    }
}