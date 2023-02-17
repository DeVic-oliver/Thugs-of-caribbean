namespace Assets.Scripts.Core.Components.Spawner
{
    using Assets.Scripts.Core.Enemies;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyTarget;

        [Header("Spawn areas")]
        [SerializeField] private List<SpawnArea> _spawnAreas;
        private int _spawnInterval;

        [Space(10f)]
        [Header("GameObjects to spawn")]
        [SerializeField] private List<EnemyBase> _enemiesToSpawn;

        private Coroutine _currentCoroutine;

        void Start()
        {
            _spawnInterval = PlayerPrefs.GetInt("ENEMEIS_SPAWN_INTERVAL");
        }

        public void StartSpawnObjects()
        {
            if(_currentCoroutine == null)
            {
                _currentCoroutine = StartCoroutine("SpawnObject");
            }
        }

        public void StopSpawning()
        {
            if( _currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }
        }

        public void ResetEnemies()
        {

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
            foreach (var area in _spawnAreas)
            {
                if (area.IsInvisible)
                {
                    EnemyBase gameObject = _enemiesToSpawn.GetRandomItem();
                    InjectEnemyTarget(gameObject, _enemyTarget);
                    var obj = GetEnemyObjectAfterCast(gameObject);
                    area.SpawnGameObject(obj);
                    break;
                }
            }
        }
        private void InjectEnemyTarget(EnemyBase enemy, GameObject target)
        {
            enemy.EnemyGameObject = target;
        }
        private GameObject GetEnemyObjectAfterCast(EnemyBase enemy)
        {
            return enemy.gameObject;
        }
    }
}
