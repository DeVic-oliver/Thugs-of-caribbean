namespace Assets.Scripts.Core.Components.Audio
{
    using UnityEngine;

    [RequireComponent(typeof(AudioSource))]
    public class UIAudioManagerBase : MonoBehaviour
    {
        private AudioSource _audioSource;

        [SerializeField] private AudioClip _buttonClick;
        [SerializeField] private AudioClip _closeSound;


        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayButtonClick()
        {
            _audioSource.PlayOneShot(_buttonClick);
        }

        public void PlayCloseSound()
        {
            _audioSource.PlayOneShot(_closeSound);
        }

    }
}