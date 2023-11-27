namespace Assets.Scripts.Core.Components.Damage
{
    using Assets.Scripts.Core.Components._2DComponents;
    using System.Collections;
    using UnityEngine;

    [RequireComponent(typeof(ExplosionSpriteChanger))]
    public class VisualDamageFeedback : MonoBehaviour
    {
        [Header("Damage settings")]
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Color _damageFeedbackColor;
        [SerializeField] private float _flashDuration = 0.1f;
        [SerializeField] private ParticleSystem _deathVFX;

        private ExplosionSpriteChanger _explosionSpriteChanger;
        private Coroutine _damageColor;

        public void FlashDamage()
        {
            if(_damageColor == null)
                _damageColor = StartCoroutine(nameof(Damage));
        }

        private IEnumerator Damage()
        {

            yield return LerpRendererColorToDamageColor();

            yield return LerpRendererColorToWhiteColor();

            _damageColor = null;
        }

        private IEnumerator LerpRendererColorToDamageColor()
        {
            yield return LerpRendererColorTo(Color.white, _damageFeedbackColor);
        }

        private IEnumerator LerpRendererColorToWhiteColor()
        {
            yield return LerpRendererColorTo(_damageFeedbackColor, Color.white);
        }

        private IEnumerator LerpRendererColorTo(Color orginColor, Color TargetColor)
        {
            float count = 0;
            while (count < _flashDuration)
            {
                _renderer.color = Color.Lerp(orginColor, TargetColor, count / _flashDuration);
                count += Time.deltaTime;
                yield return null;
            }
        }

        public void PlayDeathVFX()
        {
            _deathVFX.Play();
            _explosionSpriteChanger.PlaySpriteExplosion();
        }

        private void Awake()
        {
            _explosionSpriteChanger = GetComponent<ExplosionSpriteChanger>();
            _renderer.color = Color.white;
        }
    }
}