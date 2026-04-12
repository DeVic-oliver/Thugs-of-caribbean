using NaughtyAttributes;
using TOC.Core.Interfaces;
using UnityEngine;

namespace TOC.Core.CombatSystem
{
    [CreateAssetMenu(fileName = "CannonBall_", menuName = "TOC/Combat/Cannon Ball")]
    public class CannonBallParameters : ScriptableObject, IParameters<CannonBallParameters.CannonBallData>
    {
        #region Fields
        [SerializeField, MinValue(1f)] private float m_Damage;
        [SerializeField, MinValue(0.3f)] private float m_Speed;
        #endregion

        #region Public Methods
        public CannonBallData GetData() => new(this);
        #endregion

        #region Classes
        public class CannonBallData
        {
            #region Fields
            private readonly float m_Damage;
            private readonly float m_Speed;
            #endregion

            #region Properties
            public float Damage => m_Damage;
            public float Speed => m_Speed;
            #endregion

            #region Constructor
            public CannonBallData(CannonBallParameters parameters)
            {
                m_Damage = parameters.m_Damage;
                m_Speed = parameters.m_Speed;
            }
            #endregion

            #region Public Methods
            public void ApplyDamage(IDamageable value)
            {
                value.ApplyDamage(m_Damage);
            } 
            #endregion
        }
        #endregion
    }
}