using System;
using Source.Game.Scripts.Configure;
using Source.Game.Scripts.Spawn;
using Source.Game.Scripts.View;
using Source.Game.Scripts.Yandex;
using UnityEngine;

namespace Source.Game.Scripts.Presenter.UI
{
    public class GamePresenter : ScreenPresenter
    {
        [SerializeField] private GameView _gameView;
        [SerializeField] private ParticleSystem _confetti;
        [SerializeField] private ScorePresenter _scorePresenter;

        private const int FirstLevel = 0;

        private Config _config;
        private SpawnerBox _spawnerBox;
        private SceneLoader _sceneLoader;
        private BoardHeartPresenter _boardHeart;
        private YandexShowAds _yandexShowAds;
        private YandexLeaderBoard _yandexLeaderBoard;

        public event Action OpenedMenu;
        public event Action ResetScene;
        public event Action ResetHeart;
        public event Action LoadedNextScene;

        private void OnEnable()
        {
            _gameView.ExitButtonClick += OnCloseButtonClick;
            _gameView.LoadNextLevel += OnLoadNextLevel;
        }

        private void OnDisable()
        {
            _gameView.ExitButtonClick -= OnCloseButtonClick;
            _gameView.LoadNextLevel -= OnLoadNextLevel;
        }

        public void Init(Config config, BoardHeartPresenter boardHeart,
            SpawnerBox spawnerBox, SceneLoader sceneLoader, YandexShowAds yandexShowAds, YandexLeaderBoard yandexLeaderBoard)
        {
            _config = config;
            _boardHeart = boardHeart;
            _spawnerBox = spawnerBox;
            _sceneLoader = sceneLoader;
            _yandexShowAds = yandexShowAds;
            _yandexLeaderBoard = yandexLeaderBoard;
        }

        public void EnableStartTutorial()
        {
            if (_config.CurrentLevel == FirstLevel)
                _gameView.EnableTutorial();
        }

        public void OnAddScore(int score) =>
            _scorePresenter.AddScore(score);

        public void SetTargetScore(int score) =>
            _scorePresenter.SetTargetScore(score);

        public void OnBoxFallen() =>
            _boardHeart.TakeDamage();

        protected override void OpenScreen()
        {
            _spawnerBox.Active();
            base.OpenScreen();
        }

        protected override void CloseScreen()
        {
            _spawnerBox.Inactive();
            ResetScene?.Invoke();
            ResetHeart?.Invoke();
            base.CloseScreen();
            _config.DisableGame();
        }

        private void OnCloseButtonClick()
        {
#if YANDEX_GAMES
            _yandexShowAds.OnShowInterstitialButtonClick();
#endif
            CloseScreen();
            _gameView.DisableNextButton();
            _gameView.DisableTutorial();
            OpenedMenu?.Invoke();
        }

        public void OnLevelCompleted()
        {
            _config.DisableGame();
            _confetti.Play();
            _boardHeart.Reset();
            _gameView.EnableNextLevelButton();
            _spawnerBox.Inactive();
            _spawnerBox.Reset();
        }

        private void OnLoadNextLevel()
        {
#if YANDEX_GAMES
            _yandexShowAds.OnShowInterstitialButtonClick();
#endif
            _config.Improve();
            _sceneLoader.Load();
            _gameView.DisableNextButton();
            LoadedNextScene?.Invoke();
            _spawnerBox.Active();
            _config.EnableGame();
        }
    }
}