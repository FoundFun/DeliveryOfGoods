using System;
using UnityEngine;

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