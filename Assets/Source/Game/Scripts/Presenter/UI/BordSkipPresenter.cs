using System;
using Source.Game.Scripts.View;
using Source.Game.Scripts.Yandex;
using UnityEngine;

namespace Source.Game.Scripts.Presenter.UI
{
    public class BordSkipPresenter : MonoBehaviour
    {
        [SerializeField] private BordSkipView _view;

        private BordHeartPresenter _bordHeartPresenter;
        private YandexShowAds _yandexShowAds;

        public event Action Restart;

        private void OnEnable() => 
            _view.Restart += OnRestart;

        private void OnDisable() => 
            _view.Restart -= OnRestart;

        public void Init(BordHeartPresenter bordHeart, YandexShowAds yandexShowAds)
        {
            _bordHeartPresenter = bordHeart;
            _yandexShowAds = yandexShowAds;
        }

        private void OnRestart()
        {
#if YANDEX_GAMES
        _yandexShowAds.OnShowInterstitialButtonClick();
#endif
            Restart?.Invoke();
            _bordHeartPresenter.Recover();
        }
    }
}