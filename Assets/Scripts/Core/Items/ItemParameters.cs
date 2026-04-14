using TOC.Core.Enums;
using UnityEngine;

namespace TOC.Core.Items
{ 
    public class ItemParameters : DataObject    
    {
        #region Fields
        [SerializeField] protected RarityLevel m_Rarity;
        #endregion

        #region Properties
        public RarityLevel Rarity => m_Rarity;
        #endregion

        #region Classes
        public class ItemData
        {
            #region Fields
            protected readonly ItemParameters m_Parameters;
            #endregion

            #region Constructor
            public ItemData(ItemParameters parameters)
            {
                m_Parameters = parameters;
            }
            #endregion
        } 
        #endregion
    }
}