using NaughtyAttributes;
using System;
using TOC.Core.Enums;
using UnityEngine;

namespace TOC.Core.EntitySystem.Data
{
    [CreateAssetMenu(fileName = "EntityVisual_", menuName = "TOC/Entity/Visuals")]
    public class EntityVisualParameters : ScriptableObject
    {
        #region Fields
        [SerializeField, ShowAssetPreview] private Sprite m_MainVisual;
        [SerializeField] private EntityWellbeingVisual[] m_EntityWellbeingVisual;
        #endregion

        #region Properties
        public Sprite MainVisual => m_MainVisual;
        #endregion

        #region Public Methods
        public Sprite GetWellbeingStatusSprite(EntityWellbeingParameters.EntityWellbeingData value)
        {
            foreach (var item in m_EntityWellbeingVisual)
            {
                if (value.WellbeingStatus == item.WellbeingType)
                    return item.Sprite;
            }

            Debug.LogError("Couldn't find the sprite status");

            return null;
        } 
        #endregion

        #region Classes
        [Serializable]
        public class EntityWellbeingVisual
        {
            public EWellbeingTypes WellbeingType;
            public Sprite Sprite;
        } 
        #endregion
    }
}
