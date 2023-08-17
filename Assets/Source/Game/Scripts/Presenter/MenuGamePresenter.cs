using System;
using UnityEngine;

public class MenuGamePresenter : ScreenPresenter
{
    [SerializeField] private MenuGameView _menuGameView;

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

    private void OnStartButtonClick()
    {
        Close();
        OpenedGame?.Invoke();
    }

    private void OnSettingsButtonClick()
    {
        Close();
        OpenedSettings?.Invoke();
    }
}