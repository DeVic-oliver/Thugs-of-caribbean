using UnityEngine;

namespace TOC.Core
{
    public class DataObject : ScriptableObject
    {
        #region Fields
        [SerializeField] protected Sprite m_Icon;
        [SerializeField] protected string m_ResourceName;
        #endregion

        #region Properties
        public Sprite Icon => m_Icon;
        public string ResourceName => m_ResourceName;
        #endregion
    }
}
