using TOC.Core.Interfaces;
using TOC.Core.EntitySystem.Data;
using UnityEngine;

namespace TOC.Core.EntitySystem
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Entity : MonoBehaviour
    {
        #region Fields
        [SerializeField] private EntityIdentityParameters m_IdentityParameters;
        [SerializeField] private EntityWellbeingParameters m_WellbeingParameters;
        [SerializeField] private EntityMovementParameters m_MovementParameters;

        [Space]
        [SerializeField] private Rigidbody2D m_Rigidbody;
        
        private EntityIdentityParameters.EntityIdentityData m_IdentityData;
        private EntityWellbeingParameters.EntityWellbeingData m_WellbeingData;
        private EntityMovementParameters.EntityMovementData m_MovementData;
        #endregion
        
        #region Properties
        public EntityIdentityParameters.EntityIdentityData IdentityData => m_IdentityData;
        public EntityWellbeingParameters.EntityWellbeingData WellbeingData => m_WellbeingData;
        public EntityMovementParameters.EntityMovementData MovementData => m_MovementData;
        public Rigidbody2D Rigidbody => m_Rigidbody;
        #endregion

        #region Unity Methods
        void Start()
        {
        }

        void Update()
        {

        }
        #endregion

        #region Public Methods
        public void Init()
        {
            InitData(m_MovementParameters, ref m_MovementData);
            InitData(m_WellbeingParameters, ref m_WellbeingData);
            InitData(m_IdentityParameters, ref m_IdentityData);
        }
        #endregion

        #region Private Methods
        private void InitData<T>(object input, ref T output)
        {
            if (input == null)
            {
                Debug.LogError($"Null detected when trying to initialize at {gameObject.name}");
                output = default;
                return;
            }

            if (input is IParameters<T> parameterData)
            {
                output = parameterData.GetData();
                return;
            }

            output = default;
        }
        #endregion
    }
}