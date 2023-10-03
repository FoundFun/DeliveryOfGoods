using Agava.WebUtility;
using Source.Game.Scripts.Presenter;
using UnityEngine;

namespace Source.Game.Scripts.Yandex
{
    public class BackgroundController : MonoBehaviour
    {
        [SerializeField] private SoundPresenter _soundPresenter;
        
        private void OnEnable() => 
            WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;

        private void OnDisable() => 
            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;

        private void OnInBackgroundChange(bool inBackground)
        {
            if (inBackground)
                _soundPresenter.StopMusic();
            else
                _soundPresenter.PlayMusic();
        }
    }
}