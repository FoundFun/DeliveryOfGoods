using Agava.YandexGames;
using Source.Game.Scripts.Presenter;
using Source.Game.Scripts.Presenter.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Game.Scripts.Yandex
{
    public class YandexShowAds : MonoBehaviour
    {
        [SerializeField] private SoundChangePresenter _soundPresenter;
        [SerializeField] private BordResurrectPresenter _bordResurrectPresenter;
        [SerializeField] private Image _adsPanel;

        public void OnShowInterstitialButtonClick() =>
            InterstitialAd.Show(StopGame, StartGame);

        public void OnShowVideoButtonClick() =>
            VideoAd.Show(StopGame, _bordResurrectPresenter.OnResurrect, StartGame);

        private void StartGame(bool wasShow)
        {
            if (!wasShow)
                return;

            Time.timeScale = 1;
            _soundPresenter.PlayMusic();
            _adsPanel.fillCenter = false;
            _adsPanel.raycastTarget = false;
        }

        private void StartGame()
        {
            Time.timeScale = 1;
            _soundPresenter.PlayMusic();
            _adsPanel.fillCenter = false;
            _adsPanel.raycastTarget = false;
        }

        private void StopGame()
        {
            Time.timeScale = 0;
            _soundPresenter.StopMusic();
            _adsPanel.fillCenter = true;
            _adsPanel.raycastTarget = true;
        }
    }
}