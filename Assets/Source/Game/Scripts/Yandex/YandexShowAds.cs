using Agava.YandexGames;
using Source.Game.Scripts.Presenter;
using Source.Game.Scripts.Presenter.UI;
using UnityEngine;

namespace Source.Game.Scripts.Yandex
{
    public class YandexShowAds : MonoBehaviour
    {
        [SerializeField] private SoundPresenter _soundPresenter;

        private BoardResurrectPresenter _boardResurrectPresenter;
        private bool _isChangedMusic;

        public void OnShowInterstitialButtonClick() =>
            InterstitialAd.Show(StopGame, StartGame);

        public void OnShowVideoButtonClick() =>
            VideoAd.Show(StopGame, _boardResurrectPresenter.OnResurrect, ResumeGame);

        public void Init(BoardResurrectPresenter boardResurrectPresenter) =>
            _boardResurrectPresenter = boardResurrectPresenter;

        private void StartGame(bool wasShow)
        {
            if (!wasShow)
                return;

            ResumeGame();
        }

        private void StopGame()
        {
            Time.timeScale = 0;

            if (!_soundPresenter.IsPlay)
                return;
            
            _soundPresenter.StopMusic();
            _isChangedMusic = true;
        }

        private void ResumeGame()
        {
            Time.timeScale = 1;

            if (!_isChangedMusic) 
                return;
            
            _soundPresenter.PlayMusic();
            _isChangedMusic = false;
        }
    }
}