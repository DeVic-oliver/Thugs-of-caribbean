using UnityEngine;

namespace TOC.Core.EntitySystem
{
    [RequireComponent(typeof(Entity))]
    public class EntityMovement : MonoBehaviour
    {
        #region Fields
        [SerializeField] private Entity m_Entity;
        #endregion

        #region Properties
        public Entity Entity { get; private set; }
        #endregion

        #region Unity Methods
        private void Awake()
        {
            if (Entity == null)
            {
                if (m_Entity == null)
                {
                    Debug.LogError($"Entity reference is null at {gameObject.name} trying to fetch script!");
                    if (!TryGetComponent<Entity>(out var value))
                        Debug.LogError($"Entity reference still null at {gameObject.name}!");
                    else
                        Entity = value;
                }
                else
                    Entity = m_Entity;
            }
        }

        private void Update()
        {
            RotateEntity();
        }

        private void FixedUpdate()
        {
            MoveEntity();
        }
        #endregion

        #region Private Methods
        private void MoveEntity()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                float speed = m_Entity.MovementData.MovementSpeed;
                Vector2 dir = transform.position + (speed * Time.deltaTime * transform.up);
                m_Entity.Rigidbody.MovePosition(dir);
            }
        }
        
        private void RotateEntity()
        {
            float angle = GetAngle();
            transform.rotation = GetSlerpedRotation(angle);
        }

        private float GetAngle()
        {
            Vector2 delta = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            delta.Normalize();
            return Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
        }

        private Quaternion GetSlerpedRotation(float angle)
        {
            float clampedRange = 1f * Time.deltaTime * m_Entity.MovementData.RotationSpeed;
            float angleOffset = 90f;
            Quaternion rotation = Quaternion.Euler(0, 0, angle - angleOffset);
            return Quaternion.Slerp(transform.rotation, rotation, clampedRange);
        }
        #endregion
    }
}