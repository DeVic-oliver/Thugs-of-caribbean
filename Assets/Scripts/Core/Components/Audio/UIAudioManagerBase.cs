namespace Assets.Scripts.Core.Components.Audio
{
    using UnityEngine;

    [RequireComponent(typeof(AudioSource))]
    public class UIAudioManagerBase : MonoBehaviour
    {
        public AudioClip ButtonClick { get { return _buttonClick; }}
        public AudioClip CloseSound { get { return _closeSound; }}

        [SerializeField] private AudioClip _buttonClick;
        [SerializeField] private AudioClip _closeSound;
        [SerializeField] private AudioClip _sliderSound;

        private AudioSource _audioSource;


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

        public void PlaySliderSound()
        {
            _audioSource.PlayOneShot(_sliderSound);
        }
    }
}