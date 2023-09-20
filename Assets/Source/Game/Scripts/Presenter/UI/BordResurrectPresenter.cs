using UnityEngine;

namespace Source.Game.Scripts.Presenter.UI
{
    public class BordResurrectPresenter : MonoBehaviour
    {
        [SerializeField] private BordResurrectView _bordResurrectView;
        [SerializeField] private BordHeartPresenter _heartPresenter;
        [SerializeField] private YandexShowAds _yandexShowAds;

        private void OnEnable()
        {
            _bordResurrectView.ShowAds += OnShowAds;
        }

        private void OnDisable()
        {
            _bordResurrectView.ShowAds -= OnShowAds;
        }

        public void OnResurrect()
        {
            _heartPresenter.Recover();
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