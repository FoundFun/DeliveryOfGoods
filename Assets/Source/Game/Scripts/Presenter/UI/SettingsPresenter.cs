using System;
using UnityEngine;

namespace Source.Game.Scripts.Presenter.UI
{
    public class SettingsPresenter : ScreenPresenter
    {
        [SerializeField] private SettingsView _settingsView;

        public event Action OpenedMenu;

        private void OnEnable()
        {
            _settingsView.ExitButtonClick += OnCloseButtonClick;
        }

        private void OnDisable()
        {
            _settingsView.ExitButtonClick -= OnCloseButtonClick;
        }

        private void OnCloseButtonClick()
        {
            Close();
            OpenedMenu?.Invoke();
        }
    }
}