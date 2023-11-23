namespace Assets.Scripts.Core.Components.Spawner
{
    using Assets.Scripts.Core.Enemies;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _target;

        [Header("Spawn areas")]
        [SerializeField] private List<SpawnArea> _spawnAreas;

        [Space(10f)]
        [Header("Enemies to spawn")]
        [SerializeField] private List<EnemyBase> _enemiesList;

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
            foreach (SpawnArea area in _spawnAreas)
            {
                if (area.IsInvisible)
                {
                    GameObject obj = GetEnemyObject();
                    area.SpawnGameObject(obj);
                    break;
                }
            }
        }

        private GameObject GetEnemyObject()
        {
            EnemyBase enemy = _enemiesList.GetRandomItem();
            InjectTarget(enemy, _target);
            return enemy.gameObject;
        }

        private void InjectTarget(EnemyBase enemy, GameObject target)
        {
            enemy.EnemyGameObject = target;
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
