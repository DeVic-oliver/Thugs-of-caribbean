using TOC.Core.Enums;
using TOC.Core.Interfaces;
using UnityEngine;

namespace TOC.Core.EntitySystem.Data
{
    [CreateAssetMenu(fileName = "EntityMovement_", menuName = "TOC/Entity/Wellbeing")]
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
            public EWellbeingTypes WellbeingStatus 
            { 
                get 
                {
                    if (m_Health <= 0)
                        return EWellbeingTypes.Dead;

                    if (m_Health <= 25f)
                        return EWellbeingTypes.Critical;

                    if (m_Health <= 50f)
                        return EWellbeingTypes.Damaged;

                    if (m_Health <= 75f)
                        return EWellbeingTypes.Good;

                    return EWellbeingTypes.Healthy;
                } 
            }
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

            #region Public Methods
            public float AddHealth(float value)
            {
                AddValue(value, ref m_Health);
                return m_Health;
            }

            public float RemoveHealth(float value)
            {
                RemoveValue(value, ref m_Health);
                return m_Health;
            }

            public float AddStamina(float value)
            {
                AddValue(value, ref m_Stamina);
                return m_Stamina;
            }

            public float RemoveStamina(float value)
            {
                RemoveValue(value, ref m_Stamina);
                return m_Stamina;
            }
            #endregion

            #region Private Methods
            private void AddValue(float arg0, ref float arg1)
            {
                arg1 += arg0;
            }

            private void RemoveValue(float arg0, ref float arg1)
            {
                if (arg0 >= arg1)
                    arg1 = 0f;
                else
                    arg1 -= arg0;
            }
            #endregion
        }
        #endregion
    }
}