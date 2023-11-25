namespace Assets.Scripts.Core.Components._2DComponents
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    
    public class ExplosionSpriteChanger : MonoBehaviour
    {
        [Header("Explosion Sprite Settings")]
        [SerializeField] private SpriteRenderer _explosionGameObject;
        [SerializeField] private List<Sprite> _explosionSpriteList;
        
        private Coroutine _explosionCoroutine;
        private float _spriteChangeSpeed = 0.1f;


        public void PlaySpriteExplosion()
        {
            if (_explosionCoroutine == null)
                StartCoroutine(nameof(ChangeExplosionSpritesFowardsAndBackwards));
        }

        private IEnumerator ChangeExplosionSpritesFowardsAndBackwards()
        {
            yield return ChangeSpritesForwards();
            yield return ChangeSpriteBackwards();

            SetExplosionSpriteNull();
            SetCoroutineExplosionNull();
        }

        private IEnumerator ChangeSpritesForwards()
        {
            foreach (Sprite sprite in _explosionSpriteList)
            {
                ChangeExplostionSpriteTo(sprite);
                yield return ReturnWaitForSeconds();
            }
        }

        private IEnumerator ChangeSpriteBackwards()
        {
            for (int spriteIndex = _explosionSpriteList.Count - 1; spriteIndex > 0; spriteIndex--)
            {
                ChangeExplostionSpriteTo(_explosionSpriteList[spriteIndex]);
                yield return ReturnWaitForSeconds();
            }
        }

        private void ChangeExplostionSpriteTo(Sprite newSprite)
        {
            _explosionGameObject.sprite = newSprite;
        }

        private IEnumerator ReturnWaitForSeconds()
        {
            yield return new WaitForSeconds(_spriteChangeSpeed);
        }

        private void OnEnable()
        {
            SetExplosionSpriteNull();
        }

        private void SetExplosionSpriteNull()
        {
            _explosionGameObject.sprite = null;
        }

        private void OnDisable()
        {
            SetCoroutineExplosionNull();
        }

        private void SetCoroutineExplosionNull()
        {
            _explosionCoroutine = null;
        }
    }
}