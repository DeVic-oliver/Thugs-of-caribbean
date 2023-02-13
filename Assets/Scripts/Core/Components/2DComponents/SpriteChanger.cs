using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace Assets.Scripts.Core.Components._2DComponents
{
    public abstract class SpriteChanger : MonoBehaviour
    {
        [SerializeField] private List<SpriteDict> _spritesList;
        private Dictionary<string, Sprite> _spriteDicts;
        [SerializeField] private SpriteRenderer _renderer;

        // Use this for initialization
        protected virtual void Start()
        {
            PopulateDictionary();
        }
        private void PopulateDictionary()
        {
            foreach (var sprite in _spritesList)
            {
                _spriteDicts.Add(sprite.ID, sprite.Sprite);
            }
        }
        protected virtual void ChangeCurrentSpriteTo(string ID)
        {
            var sprite = GetSprite(ID);
            _renderer.sprite = sprite;
        }
        protected virtual Sprite GetSprite(string ID)
        {
            return _spriteDicts[ID];
        }

    }

    [System.Serializable]
    class SpriteDict
    {
        public string ID;
        public Sprite Sprite;
    }

}