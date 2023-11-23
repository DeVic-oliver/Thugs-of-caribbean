namespace Assets.Scripts.Core.Components.Damage
{
    using DG.Tweening;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;
    
    public class DamageComponent : MonoBehaviour
    {
        [Header("Damage settings")]
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Color _damageFeedbackColor;
        [SerializeField] private float _flashSpeed = 1f;
        [SerializeField] private ParticleSystem _deathVFX;

        [Header("Explosion Sprite Settings")]
        [SerializeField] private SpriteRenderer _explosionGameObject;
        [SerializeField] private List<Sprite> _explosionSpriteList;
        private Coroutine _explosionCoroutine;
        private float _spriteChangeSpeed = 0.1f;

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
            foreach (var sprite in _explosionSpriteList)
            {
                _explosionGameObject.sprite = sprite;
                yield return new WaitForSeconds(_spriteChangeSpeed);
            }

            for (int spriteIndex = _explosionSpriteList.Count - 1; spriteIndex > 0; spriteIndex--)
            {
                _explosionGameObject.sprite = _explosionSpriteList[spriteIndex];
                yield return new WaitForSeconds(_spriteChangeSpeed);
            }

            _explosionGameObject.sprite = null;
            _explosionCoroutine = null;
        }

        private void Start()
        {
            if (_currentColorTween != null)
                _currentColorTween.Kill();
        }
    }
}