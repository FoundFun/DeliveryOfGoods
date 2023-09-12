using System;
using DeliveryOfGoods.Model;
using UnityEngine;

public class MenuGamePresenter : ScreenPresenter
{
    [SerializeField] private MenuGameView _menuGameView;
    [SerializeField] private Config _config;

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
        _config.EnableGame();
    }

    private void OnSettingsButtonClick()
    {
        Close();
        OpenedSettings?.Invoke();
    }
}