using Source.Game.Scripts.View;
using Source.Game.Scripts.Yandex;
using UnityEngine;
using UnityEngine.Serialization;

namespace Source.Game.Scripts.Presenter.UI
{
    public class BoardResurrectPresenter : MonoBehaviour
    {
        [FormerlySerializedAs("_bordResurrectView")] [SerializeField] private BoardResurrectView _boardResurrectView;

        private BoardHeartPresenter _boardHeartPresenter;
        private YandexShowAds _yandexShowAds;

        private void OnEnable() => 
            _boardResurrectView.ShowAds += OnShowAds;

        private void OnDisable() => 
            _boardResurrectView.ShowAds -= OnShowAds;

        public void Init(BoardHeartPresenter boardHeart, YandexShowAds yandexShowAds)
        {
            _boardHeartPresenter = boardHeart;
            _yandexShowAds = yandexShowAds;
        }

        public void OnResurrect() => 
            _boardHeartPresenter.Recover();

        public void ActiveRestartButton() => 
            _boardResurrectView.ActiveRestartButton();

        private void OnShowAds()
        {
#if YANDEX_GAMES
        _yandexShowAds.OnShowVideoButtonClick();
#endif
            _boardResurrectView.InactiveRestartButton();
        }
    }
}