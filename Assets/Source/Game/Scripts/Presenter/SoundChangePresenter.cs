using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Source.Game.Scripts.Presenter
{
    public class SoundChangePresenter : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup _master;
        [SerializeField] private Toggle _toggle;

        private const string MasterVolume = "MasterVolume";

        private void OnEnable() => 
            _toggle.onValueChanged.AddListener(ToggleMusic);

        private void OnDisable() =>
            _toggle.onValueChanged.RemoveListener(ToggleMusic);

        public void PlayMusic()
        {
            const float volume = 0f;

            _master.audioMixer.SetFloat(MasterVolume, volume);
        }

        public void StopMusic()
        {
            const float volume = -80f;

            _master.audioMixer.SetFloat(MasterVolume, volume);
        }

        private void ToggleMusic(bool isEnabled)
        {
            if (isEnabled)
                StopMusic();
            else
                PlayMusic();
        }
    }
}