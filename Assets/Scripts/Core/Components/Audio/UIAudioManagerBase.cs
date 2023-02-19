namespace Assets.Scripts.Core.Components.Audio
{
    using UnityEngine;

    [RequireComponent(typeof(AudioSource))]
    public class UIAudioManagerBase : MonoBehaviour
    {
        private AudioSource _audioSource;

        [SerializeField] private AudioClip _buttonClick;
        public AudioClip ButtonClick { get { return _buttonClick; }}

        [SerializeField] private AudioClip _closeSound;
        public AudioClip CloseSound { get { return _closeSound; }}

        [SerializeField] private AudioClip _sliderSound;


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