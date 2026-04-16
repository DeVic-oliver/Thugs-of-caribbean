using TOC.Core.EntitySystem;
using UnityEngine;

namespace TOC.Core.Items
{ 
    public class InteractableItem : MonoBehaviour
    {
        #region Fields
        [SerializeField] private SpriteRenderer m_Renderer;

        private ItemParameters.ItemData m_Data;
        #endregion

        #region Properties
        public ItemParameters.ItemData Data => m_Data;
        #endregion

        #region Public Methods
        public void Setup(ItemParameters.ItemData value)
        {
            m_Data = value;
            m_Renderer.sprite = value.Parameters.Icon;
        } 

        public void Use(Entity entity)
        {
            if (entity)
            {
                if (entity.IsPlayer)
                    m_Data?.Use(entity);
                
                Destroy(gameObject);
            }
        }
        #endregion
    }
}