using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core.Components.Damage
{
    public class DamageComponent : MonoBehaviour
    {
        [Header("Damage settings")]
        [SerializeField] private MeshRenderer _enemyMeshRenderer;
        [SerializeField] private Color _damageFeedbackColor;
        [SerializeField] private float _flashSpeed = 1f;

        public List<VFXDict> _vfxDict = new List<VFXDict>();
        private Dictionary<string, ParticleSystem> dict = new();

        private Tween _currentColorTween;

        public void FlashShader()
        {
            if (!_currentColorTween.IsActive())
            {
                _currentColorTween = _enemyMeshRenderer.material.DOColor(_damageFeedbackColor, "_EmissionColor", _flashSpeed).SetLoops(2, LoopType.Yoyo);
            }
        }

        public ParticleSystem GetVFXFromDict(string id)
        {
            if (dict.ContainsKey(id))
            {
                return dict[id];
            }

            return null;
        }

        private void Start()
        {
            PopulateVFXDictionary();
        }
        private void PopulateVFXDictionary()
        {
            foreach (var item in _vfxDict)
            {
                dict.Add(item.ID, item.VFX);
            }
        }
    }

    [System.Serializable]
    public class VFXDict
    {
        public string ID;
        public ParticleSystem VFX;
    }
}