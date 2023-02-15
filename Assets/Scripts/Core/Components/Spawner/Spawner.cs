namespace Assets.Scripts.Core.Components.Spawner
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Spawner : MonoBehaviour
    {
        [Header("Spawn areas")]
        [SerializeField] private List<SpawnArea> _spawnAreas;
        private int _spawnInterval;

        [Space(10f)]
        [Header("GameObjects to spawn")]
        [SerializeField] private List<GameObject> _gameObjectsToSpawn;

        private Coroutine _currentCoroutine;

        void Start()
        {
            _spawnInterval = PlayerPrefs.GetInt("ENEMEIS_SPAWN_INTERVAL");
            StartSpawnObjects();
        }

        public void StartSpawnObjects()
        {
            if(_currentCoroutine == null)
            {
                Debug.Log("COROUTINE STARTED");
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
        private IEnumerator SpawnObject()
        {
            GameObject gameObject = _gameObjectsToSpawn.GetRandomItem();
            var counter = 0f;
            while (counter <= 30f)
            {
                counter += Time.deltaTime;
                foreach (var area in _spawnAreas)
                {
                    Debug.Log("SEARCHING FOR AREA");
                    if (area.IsInvisible)
                    {
                        Debug.Log("AREA FOUND");
                        area.SpawnGameObject(gameObject);
                        break;
                    }
                }
                yield return new WaitForSeconds(_spawnInterval);
            }
        }
    }
}
