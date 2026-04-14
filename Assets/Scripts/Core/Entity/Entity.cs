using TOC.Core.Interfaces;
using TOC.Core.EntitySystem.Data;
using UnityEngine;

namespace TOC.Core.EntitySystem
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Entity : MonoBehaviour, IDamageable
    {
        #region Fields
        [SerializeField] private EntityIdentityParameters m_IdentityParameters;
        [SerializeField] private EntityWellbeingParameters m_WellbeingParameters;
        [SerializeField] private EntityMovementParameters m_MovementParameters;

        [Space]
        [SerializeField] private Rigidbody2D m_Rigidbody;
        [SerializeField] private SpriteRenderer m_SailRenderer;
        
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
        private void Awake()
        {
            Init();    
        }
        #endregion

        #region Public Methods
        public void Init()
        {
            InitData(m_MovementParameters, ref m_MovementData);
            InitData(m_WellbeingParameters, ref m_WellbeingData);
            InitData(m_IdentityParameters, ref m_IdentityData);

            UpdateSailSprite();
        }

        public float ApplyDamage(float value) => RemoveHealth(value);
        public float AddHealth(float value)
        {
          float h = m_WellbeingData.AddHealth(value);
            UpdateSailSprite();
            return h;  
        }
        public float RemoveHealth(float value)
        {
            float h = m_WellbeingData.RemoveHealth(value);
            UpdateSailSprite();
            return h;
        }
        public float AddStamina(float value) => m_WellbeingData.AddStamina(value);
        public float RemoveStamina(float value) => m_WellbeingData.RemoveStamina(value);
        #endregion

        #region Private Methods
        private void InitData<T>(object input, ref T output)
        {
            if (input == null)
            {
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

        private void UpdateSailSprite()
        {
            m_SailRenderer.sprite = m_IdentityData.Visuals.GetWellbeingStatusSprite(m_WellbeingData);
        }
        #endregion
    }
}