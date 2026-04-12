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
            private EntityMovementParameters m_Parameters;
            #endregion

            #region Properties
            public float MovementSpeed => m_Parameters.m_MovementSpeed;
            public float RotationSpeed => m_Parameters.m_RotationSpeed;
            #endregion

            #region Constructors
            public EntityMovementData(EntityMovementParameters parameters)
            {
                m_Parameters = parameters;
            }
            #endregion
        }
        #endregion
    }
}