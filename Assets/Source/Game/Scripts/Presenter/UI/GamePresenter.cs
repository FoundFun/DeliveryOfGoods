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
        private const int FirstLevel = 1;
        [SerializeField] private ParticleSystem _confetti;
        [SerializeField] private GameView _gameView;

        private Config _config;
        private SpawnerBox _spawnerBox;
        private SceneLoader _sceneLoader;
        private BordHeartPresenter _bordHeart;
        private YandexShowAds _yandexShowAds;

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

        public void Init(Config config, BordHeartPresenter bordHeart,
            SpawnerBox spawnerBox, SceneLoader sceneLoader, YandexShowAds yandexShowAds)
        {
            _config = config;
            _bordHeart = bordHeart;
            _spawnerBox = spawnerBox;
            _sceneLoader = sceneLoader;
            _yandexShowAds = yandexShowAds;
            _gameView.Init();
        }

        public void EnableStartTutorial()
        {
            if (_config.CurrentLevel == FirstLevel) 
                _gameView.EnableTutorial();
        }

        public void OnAddScore(int score) => 
            _gameView.AddScore(score);

        public void SetTargetScore(int score) => 
            _gameView.SetTargetScore(score);

        public void OnBoxFallen() => 
            _bordHeart.TakeDamage();

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
            _bordHeart.Reset();
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