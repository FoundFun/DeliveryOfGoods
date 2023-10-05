using System;
using Source.Game.Scripts.Configure;
using Source.Game.Scripts.View;
using UnityEngine;

namespace Source.Game.Scripts.Presenter.UI
{
    public class MenuGamePresenter : ScreenPresenter
    {
        [SerializeField] private MenuGameView _menuGameView;

        private Config _config;

        public event Action OpenedGame;

        private void OnEnable() => 
            _menuGameView.StartButtonClick += OnStartButtonClick;

        private void OnDisable() => 
            _menuGameView.StartButtonClick -= OnStartButtonClick;

        public void Init(Config config) =>
            _config = config;

        protected override void OpenScreen()
        {
            base.OpenScreen();
            _menuGameView.StartAnimationText();
        }

        private void OnStartButtonClick()
        {
            Close();
            _menuGameView.StopAnimationText();
            OpenedGame?.Invoke();
            _config.EnableGame();
        }
    }
}