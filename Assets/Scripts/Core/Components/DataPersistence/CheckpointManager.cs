using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core.Components.DataPersistence
{
    public class CheckpointManager : MonoBehaviour
    {
        [SerializeField] private List<CheckpointDict> _checkpointDict;
        private Dictionary<int, Checkpoint> _dictionary = new();
        
        [SerializeField] private GameObject _player;


        private int _lastCheckpointID;
        private Vector3 _lastCheckpointPosition;

        // Use this for initialization
        void Awake()
        {
            PopulateDictionaryWithCheckpoints();
            _lastCheckpointID = CheckpointSaveHandler.GetLastCheckpointID();
            RespawPlayerFromLastCheckpointIfExist();
        }
        private void PopulateDictionaryWithCheckpoints()
        {
            foreach (CheckpointDict checkpoint in _checkpointDict)
            {
                ApplyIDToCheckpointInstance(checkpoint);
                _dictionary.Add(checkpoint.ID, checkpoint.Checkpoint);
            }
        }
        private void ApplyIDToCheckpointInstance(CheckpointDict instance)
        {
            instance.Checkpoint.ID = instance.ID;
        }

        private void RespawPlayerFromLastCheckpointIfExist()
        {
            if(_lastCheckpointID != 0)
            {
                Vector3 position = _dictionary[_lastCheckpointID].transform.position;
                _player.transform.position = new Vector3(position.x, _player.transform.position.y, position.z);
            }
        }
    }

    [System.Serializable]
    class CheckpointDict
    {
        public int ID;
        public Checkpoint Checkpoint;
    }
}