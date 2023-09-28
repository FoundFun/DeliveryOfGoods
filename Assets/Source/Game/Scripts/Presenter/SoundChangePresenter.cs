using Source.Game.Scripts.Configure;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Source.Game.Scripts.Presenter
{
    public class SoundChangePresenter : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup _master;
        [SerializeField] private Toggle _toggle;

        private Config _config;

        private void OnEnable() => 
            _toggle.onValueChanged.AddListener(ToggleMusic);

        private void OnDisable() => 
            _toggle.onValueChanged.RemoveListener(ToggleMusic);

        public void Init(Config config) => 
            _config = config;

        public void PlayMusic()
        {
            _config.SetSoundVolume(0f);
            _master.audioMixer.SetFloat("MasterVolume", _config.SoundVolume);
        }

        public void StopMusic()
        {
            _config.SetSoundVolume(-80f);
            _master.audioMixer.SetFloat("MasterVolume", _config.SoundVolume);
        }

        private void ToggleMusic(bool isEnabled)
        {
            if (isEnabled)
                PlayMusic();
            else
                StopMusic();
        }
    }
}