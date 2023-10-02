using Agava.YandexGames;
using Source.Game.Scripts.Presenter;
using Source.Game.Scripts.Presenter.UI;
using UnityEngine;

namespace Source.Game.Scripts.Yandex
{
    public class YandexShowAds : MonoBehaviour
    {
        [SerializeField] private SoundChangePresenter _soundPresenter;
        
        private BoardResurrectPresenter _boardResurrectPresenter;

        public void OnShowInterstitialButtonClick() =>
            InterstitialAd.Show(StopGame, StartGame);

        public void OnShowVideoButtonClick() =>
            VideoAd.Show(StopGame, _boardResurrectPresenter.OnResurrect, StartGame);

        public void Init(BoardResurrectPresenter boardResurrectPresenter) => 
            _boardResurrectPresenter = boardResurrectPresenter;

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