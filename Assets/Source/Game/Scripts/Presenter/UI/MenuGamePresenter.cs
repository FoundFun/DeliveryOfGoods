using System;
using Source.Game.Scripts.Configure;
using UnityEngine;

namespace Source.Game.Scripts.Presenter.UI
{
    public class MenuGamePresenter : ScreenPresenter
    {
        [SerializeField] private MenuGameView _menuGameView;

        private Config _config;

        public event Action OpenedGame;
        public event Action OpenedSettings;

        private void OnEnable()
        {
            _menuGameView.StartButtonClick += OnStartButtonClick;
            _menuGameView.SettingsButtonClick += OnSettingsButtonClick;
        }

        private void OnDisable()
        {
            _menuGameView.StartButtonClick -= OnStartButtonClick;
            _menuGameView.SettingsButtonClick -= OnSettingsButtonClick;
        }

        public void Init(Config config) =>
            _config = config;

        private void OnStartButtonClick()
        {
            Close();
            OpenedGame?.Invoke();
            _config.EnableGame();
        }

        private void OnSettingsButtonClick()
        {
            Close();
            OpenedSettings?.Invoke();
        }
    }
}