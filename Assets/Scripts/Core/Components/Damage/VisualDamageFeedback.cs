namespace Assets.Scripts.Core.Components.Damage
{
    using DG.Tweening;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;
    
    public class VisualDamageFeedback : MonoBehaviour
    {
        [Header("Damage settings")]
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Color _damageFeedbackColor;
        [SerializeField] private float _flashSpeed = 1f;
        [SerializeField] private ParticleSystem _deathVFX;

        [Header("Explosion Sprite Settings")]
        [SerializeField] private SpriteRenderer _explosionGameObject;
        [SerializeField] private List<Sprite> _explosionSpriteList;
        [SerializeField] private float _spriteChangeSpeed = 0.1f;
        
        private Coroutine _explosionCoroutine;
        private Tween _currentColorTween;


        public void FlashShader()
        {
            if (!_currentColorTween.IsActive())
                _currentColorTween = _renderer.DOColor(_damageFeedbackColor, _flashSpeed).SetLoops(2, LoopType.Yoyo);
        }

        public void PlayDeathVFX()
        {
            _deathVFX.Play();
            PlaySpriteExplosion();
        }

        private void PlaySpriteExplosion() 
        {
            if(_explosionCoroutine == null)
                StartCoroutine(nameof(ChangeExplosionSpritesFowardsAndBackwards));
        }

        private IEnumerator ChangeExplosionSpritesFowardsAndBackwards()
        {
            yield return ChangeSpritesForwards();
            yield return ChangeSpriteBackwards();

            _explosionGameObject.sprite = null;
            _explosionCoroutine = null;
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

        private void Start()
        {
            if (_currentColorTween != null)
                _currentColorTween.Kill();
        }
    }
}