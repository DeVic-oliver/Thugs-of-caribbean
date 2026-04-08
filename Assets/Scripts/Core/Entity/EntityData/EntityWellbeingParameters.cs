using TOC.Core.Interfaces;
using UnityEngine;

namespace TOC.Core.EntitySystem.Data
{
    [CreateAssetMenu(fileName = "EntityMovement_Parameters_", menuName = "TOC/Entity/Wellbeing")]
    public class EntityWellbeingParameters : ScriptableObject, IParameters<EntityWellbeingParameters.EntityWellbeingData>
    {
        #region Fields
        [SerializeField, Min(0f)] private float m_Health = 100f;
        [SerializeField, Min(0f)] private float m_Stamina = 100f;
        #endregion
        
        #region Public Methods
        public EntityWellbeingData GetData() => new(this);
        #endregion

        #region Classes
        public class EntityWellbeingData
        {
            #region Fields
            private float m_Health;
            private float m_Stamina;
            #endregion

            #region Properties
            public float Health => m_Health;
            public float Stamina => m_Stamina;
            #endregion

            #region Constructors
            public EntityWellbeingData(EntityWellbeingParameters parameters)
            {
                m_Health = parameters.m_Health;
                m_Stamina = parameters.m_Stamina;
            }

            public EntityWellbeingData(float health, float stamina)
            {
                m_Health = health;
                m_Stamina = stamina;
            }
            #endregion
        } 
        #endregion
    }
}