using TOC.Core.Interfaces;
using UnityEngine;

namespace TOC.Core.EntitySystem.Data
{
    [CreateAssetMenu(fileName = "EntityIdentity_", menuName = "TOC/Entity/Identity")]
    public class EntityIdentityParameters : ScriptableObject, IParameters<EntityIdentityParameters.EntityIdentityData>
    {
        #region Fields
        [SerializeField] private string m_EntityName;
        [SerializeField] private EntityVisualParameters m_Visuals;
        #endregion

        #region Properties
        public EntityVisualParameters Visuals => m_Visuals;
        #endregion

        #region Public Methods
        public EntityIdentityData GetData() => new(this);
        #endregion

        #region Classes
        public class EntityIdentityData
        {
            #region Fields
            private string m_Name;
            private EntityVisualParameters m_Visuals;
            #endregion

            #region Properties
            public string Name => m_Name;
            public EntityVisualParameters Visuals => m_Visuals;
            #endregion

            #region Constructors
            public EntityIdentityData(EntityIdentityParameters parameters)
            {
                m_Name = parameters.m_EntityName;
                m_Visuals = parameters.m_Visuals;
            }

            public EntityIdentityData(string name, EntityVisualParameters visuals)
            {
                m_Name = name;
                m_Visuals = visuals;
            }
            #endregion
        }
        #endregion
    }
}