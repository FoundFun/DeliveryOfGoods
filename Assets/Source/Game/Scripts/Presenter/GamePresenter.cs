using System;
using UnityEngine;

public class GamePresenter : ScreenPresenter
{
    [SerializeField] private GameView _gameView;
    [SerializeField] private SpawnerBox _spawnerBox;

    public event Action OpenedMenu;

    private void OnEnable()
    {
        _gameView.ExitButtonClick += OnCloseButtonClick;
    }

    private void OnDisable()
    {
        _gameView.ExitButtonClick -= OnCloseButtonClick;
    }

    protected override void OpenScreen()
    {
        _spawnerBox.Active();
        base.OpenScreen();
    }

    protected override void CloseScreen()
    {
        _spawnerBox.Inactive();
        base.CloseScreen();
    }

    private void OnCloseButtonClick()
    {
        Close();
        OpenedMenu?.Invoke();
    }
}