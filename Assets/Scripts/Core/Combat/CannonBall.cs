using System.Collections;
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

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.TryGetComponent<IDamageable>(out var damageable))
                return;

            Debug.Log($"HIT: {other.gameObject.name}");
            m_Data.ApplyDamage(damageable);
            Destroy(gameObject);
        }
        #endregion

        #region Events
        private IEnumerator OnWait()
        {
            yield return new WaitForSeconds(8f);
            Destroy(gameObject);
        }
        #endregion
    }
}