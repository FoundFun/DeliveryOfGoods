using System;
using Source.Game.Scripts.View;
using Source.Game.Scripts.Yandex;
using UnityEngine;

namespace Source.Game.Scripts.Presenter.UI
{
    public class BoardSkipPresenter : MonoBehaviour
    {
        [SerializeField] private BoardSkipView _view;

        private BoardHeartPresenter _boardHeartPresenter;
        private YandexShowAds _yandexShowAds;

        public event Action Restart;

        private void OnEnable() => 
            _view.Restart += OnRestart;

        private void OnDisable() => 
            _view.Restart -= OnRestart;

        public void Init(BoardHeartPresenter boardHeart, YandexShowAds yandexShowAds)
        {
            _boardHeartPresenter = boardHeart;
            _yandexShowAds = yandexShowAds;
        }

        public void ActiveRestartButton() => 
            _view.ActiveRestartButton();

        private void OnRestart()
        {
#if YANDEX_GAMES
        _yandexShowAds.OnShowInterstitialButtonClick();
#endif
            Restart?.Invoke();
            _boardHeartPresenter.Recover();
        }
    }
}