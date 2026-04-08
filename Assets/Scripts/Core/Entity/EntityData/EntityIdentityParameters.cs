using TOC.Core.Interfaces;
using UnityEngine;

namespace TOC.Core.EntitySystem.Data
{
    [CreateAssetMenu(fileName = "EntityIdentity_Parameters_", menuName = "TOC/Entity/Identity")]
    public class EntityIdentityParameters : ScriptableObject, IParameters<EntityIdentityParameters.EntityIdentityData>
    {
        #region Fields
        [SerializeField] private string m_EntityName;
        [SerializeField] private Sprite m_Sprite;
        #endregion

        #region Public Methods
        public EntityIdentityData GetData() => new(this);
        #endregion

        #region Classes
        public class EntityIdentityData
        {
            #region Fields
            private string m_Name;
            private Sprite m_Sprite;
            #endregion

            #region Properties
            public string Name => m_Name;
            public Sprite Sprite => m_Sprite;
            #endregion

            #region Constructors
            public EntityIdentityData(EntityIdentityParameters parameters)
            {
                m_Name = parameters.m_EntityName;
                m_Sprite = parameters.m_Sprite;
            }

            public EntityIdentityData(string name, Sprite sprite)
            {
                m_Name = name;
                m_Sprite = sprite;
            }
            #endregion
        }
        #endregion
    }
}