using Agava.WebUtility;
using Source.Game.Scripts.Presenter;
using UnityEngine;

namespace Source.Game.Scripts.Yandex
{
    public class BackgroundController : MonoBehaviour
    {
        [SerializeField] private SoundPresenter _soundPresenter;
        
        private bool _isChangedMusic;
        
        private void OnEnable() => 
            WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;

        private void OnDisable() => 
            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;

        private void OnInBackgroundChange(bool inBackground)
        {
            if (inBackground)
            {
                Time.timeScale = 0;
                
                if (_soundPresenter.IsPlay)
                {
                    _isChangedMusic = true;
                    _soundPresenter.StopMusic();
                }
            }

            if (!inBackground)
            {
                Time.timeScale = 1;

                if (_isChangedMusic)
                {
                    _isChangedMusic = false;
                    _soundPresenter.PlayMusic();
                }
            }
        }
    }
}