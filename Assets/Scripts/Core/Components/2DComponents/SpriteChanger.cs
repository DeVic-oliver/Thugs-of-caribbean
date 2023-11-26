namespace Assets.Scripts.Core.Components._2DComponents
{
    using System.Collections.Generic;
    using UnityEngine;
    
    public abstract class SpriteChanger<T> : MonoBehaviour
    {
        [SerializeField] private List<SpriteDict<T>> _spritesList;
        [SerializeField] private SpriteRenderer _renderer;

        private Dictionary<T, Sprite> _spriteDicts = new();


        protected virtual void Start()
        {
            PopulateDictionary();
        }

        private void PopulateDictionary()
        {
            foreach (SpriteDict<T> sprite in _spritesList)
                _spriteDicts.Add(sprite.ID, sprite.Sprite);
        }

        protected virtual void ChangeCurrentSpriteTo(T ID)
        {
            var sprite = GetSprite(ID);
            _renderer.sprite = sprite;
        }

        protected virtual Sprite GetSprite(T ID)
        {
            return _spriteDicts[ID];
        }

    }

    [System.Serializable]
    public class SpriteDict<T>
    {
        public T ID;
        public Sprite Sprite;
    }

}