using TOC.Core.Interfaces;
using UnityEngine;

namespace TOC.Core.CombatSystem
{
    public class CannonBall : MonoBehaviour
    {
        #region Fields
        [SerializeField] private CannonBallParameters m_Parameters;

        private CannonBallParameters.CannonBallData m_Data;
        #endregion

        #region Unity Methods
        private void OnEnable()
        {
            m_Data = m_Parameters.GetData();
        }

        private void Update()
        {
            transform.position += m_Data.Speed * Time.deltaTime * transform.up;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision is not IDamageable damageable)
                return;

            Debug.Log($"HIT: {collision.gameObject.name}");
            m_Data.ApplyDamage(damageable);
        }
        #endregion
    }
}