using Source.Game.Scripts.View;
using UnityEngine;

namespace Source.Game.Scripts.Presenter.UI
{
    public class BordResurrectPresenter : MonoBehaviour
    {
        [SerializeField] private BordResurrectView _bordResurrectView;

        private BordHeartPresenter _bordHeartPresenter;
        private YandexShowAds _yandexShowAds;

        private void OnEnable()
        {
            _bordResurrectView.ShowAds += OnShowAds;
        }

        private void OnDisable()
        {
            _bordResurrectView.ShowAds -= OnShowAds;
        }

        public void Init(BordHeartPresenter bordHeart, YandexShowAds yandexShowAds)
        {
            _bordHeartPresenter = bordHeart;
            _yandexShowAds = yandexShowAds;
        }

    public void OnResurrect()
        {
            _bordHeartPresenter.Recover();
        }

        private void OnShowAds()
        {
#if !UNITY_EDITOR
        _yandexShowAds.OnShowVideoButtonClick();
#endif
            OnResurrect();
        }
    }
}