namespace Assets.Scripts.Core.Components.Damage
{
    using Assets.Scripts.Core.Components._2DComponents;
    using DG.Tweening;
    using UnityEditor;
    using UnityEngine;

    [RequireComponent(typeof(ExplosionSpriteChanger))]
    public class VisualDamageFeedback : MonoBehaviour
    {
        [Header("Damage settings")]
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Color _damageFeedbackColor;
        [SerializeField] private float _flashSpeed = 1f;
        [SerializeField] private ParticleSystem _deathVFX;

        private ExplosionSpriteChanger _explosionSpriteChanger;
        private Tween _currentColorTween;


        public void FlashDamage()
        {
            if (!_currentColorTween.IsActive())
                _currentColorTween = _renderer.DOColor(_damageFeedbackColor, _flashSpeed).SetLoops(2, LoopType.Yoyo);
        }

        public void PlayDeathVFX()
        {
            _deathVFX.Play();
            _explosionSpriteChanger.PlaySpriteExplosion();
        }

        private void Awake()
        {
            _explosionSpriteChanger = GetComponent<ExplosionSpriteChanger>();
        }

        private void Start()
        {
            if (_currentColorTween != null)
                _currentColorTween.Kill();
        }
    }
}