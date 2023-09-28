using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Source.Game.Scripts.Presenter
{
    public class SoundChangePresenter : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup _master;
        [SerializeField] private Toggle _toggle;

        private void OnEnable() => 
            _toggle.onValueChanged.AddListener(ToggleMusic);

        private void OnDisable() => 
            _toggle.onValueChanged.RemoveListener(ToggleMusic);

        public void PlayMusic() => 
            _master.audioMixer.SetFloat("MasterVolume", 0f);

        public void StopMusic() => 
            _master.audioMixer.SetFloat("MasterVolume", -80f);

        private void ToggleMusic(bool isEnabled)
        {
            if (isEnabled)
                PlayMusic();
            else
                StopMusic();
        }
    }
}