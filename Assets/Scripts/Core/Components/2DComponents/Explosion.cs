using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core.Components._2DComponents
{
    public class Explosion : MonoBehaviour
    {
        [Header("Explosion Sprite Settings")]
        [SerializeField] private SpriteRenderer _explosionGameObject;
        [SerializeField] private List<Sprite> _explosionSpriteList;
        private Coroutine _explosionCoroutine;
        private float _spriteChangeSpeed = 0.1f;


        public void PlaySpriteExplosition()
        {
            if (_explosionCoroutine == null)
            {
                StartCoroutine("ChangeExplosionSpritesFowardsAndBackwards");
            }
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
    }
}