using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TOC.Core.SpawnSystem
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _target;

        private Coroutine _currentCoroutine;
        private int _spawnInterval;


        public void StartSpawnObjects()
        {
            if (_currentCoroutine == null)
                _currentCoroutine = StartCoroutine(nameof(SpawnObject));
        }

        private IEnumerator SpawnObject()
        {
            while (true)
            {
                SearchForInvisibleAreaToSpawnRandomEnemy();
                yield return new WaitForSeconds(_spawnInterval);
            }
        }

        private void SearchForInvisibleAreaToSpawnRandomEnemy()
        {
        }

        private GameObject GetEnemyObject()
        {
            return null;
        }

        public void StopSpawning()
        {
            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);
        }

        void Start()
        {
            SetTheEnemiesSpawnInterval();
        }

        private void SetTheEnemiesSpawnInterval()
        {
            _spawnInterval = PlayerPrefs.GetInt("ENEMEIS_SPAWN_INTERVAL");
        }
    }
}
