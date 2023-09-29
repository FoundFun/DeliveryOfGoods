using Agava.YandexGames;
using Source.Game.Scripts.Presenter;
using Source.Game.Scripts.Presenter.UI;
using UnityEngine;

namespace Source.Game.Scripts.Yandex
{
    public class YandexShowAds : MonoBehaviour
    {
        [SerializeField] private SoundChangePresenter _soundPresenter;
        [SerializeField] private BordResurrectPresenter _bordResurrectPresenter;

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
        }

        private void StartGame()
        {
            Time.timeScale = 1;
            _soundPresenter.PlayMusic();
        }

        private void StopGame()
        {
            Time.timeScale = 0;
            _soundPresenter.StopMusic();
        }
    }
}