using Agava.YandexGames;
using Source.Game.Scripts.Presenter.UI;
using UnityEngine;

namespace Source.Game.Scripts
{
    public class YandexShowAds : MonoBehaviour
    {
        [SerializeField] private AudioSource _gameMusic;
        [SerializeField] private BordResurrectPresenter _bordResurrectPresenter;

        public void OnShowInterstitialButtonClick()
        {
            InterstitialAd.Show(StopGame, StartGame);
        }

        public void OnShowVideoButtonClick()
        {
            VideoAd.Show(StopGame, _bordResurrectPresenter.OnResurrect, StartGame);
        }

        private void StartGame(bool wasShow)
        {
            if (wasShow)
            {
                Time.timeScale = 1;
                _gameMusic.mute = false;
            }
        }

        private void StartGame()
        {
            Time.timeScale = 1;
            _gameMusic.mute = false;
        }

        private void StopGame()
        {
            Time.timeScale = 0;
            _gameMusic.mute = true;
        }
    }
}
