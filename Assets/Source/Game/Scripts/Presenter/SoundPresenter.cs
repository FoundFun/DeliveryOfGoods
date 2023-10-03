using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Source.Game.Scripts.Presenter
{
    public class SoundPresenter : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup _master;
        [SerializeField] private Toggle _toggle;

        private const string MasterVolume = "MasterVolume";
        private const float OnVolume = 0;
        private const float OffVolume = -80f;

        private float _currentVolume;

        public bool IsPlay => _currentVolume == OnVolume;

        private void OnEnable() => 
            _toggle.onValueChanged.AddListener(ToggleMusic);

        private void OnDisable() =>
            _toggle.onValueChanged.RemoveListener(ToggleMusic);

        public void PlayMusic()
        {
            _currentVolume = OnVolume;
            _master.audioMixer.SetFloat(MasterVolume, _currentVolume);
        }

        public void StopMusic()
        {
            _currentVolume = OffVolume;
            _master.audioMixer.SetFloat(MasterVolume, _currentVolume);
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