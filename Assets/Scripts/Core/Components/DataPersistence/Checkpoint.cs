using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Core.Components.DataPersistence
{
    public class Checkpoint : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        private Collider _collider;
        public int ID { get; set; }


        private void Start()
        {
            _collider = GetComponent<Collider>();

            if(!_collider.enabled)
            {
                _collider.enabled = true;
            }

            TurnOffColor();
        }

        private void OnTriggerEnter(Collider other)
        {
            string tag = other.gameObject.tag;
            
            if(tag == "Player")
            {
                TurnOn();
            }
        
        }
        private void TurnOn()
        {
            TurnOnColor();
            CheckpointSaveHandler.SaveCheckpoint(this);
        }
        private void TurnOnColor()
        {
            _meshRenderer.material.SetColor("_EmissionColor", Color.white);
        }
        private void TurnOffColor()
        {
            _meshRenderer.material.SetColor("_EmissionColor", Color.gray);
        }
        private void DisableCheckpointFuncionality()
        {
            _collider.enabled = false;
        }


    }
}