using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePresenter : ScreenPresenter
{
    [SerializeField] private GameView _gameView;

    public event Action OpenedMenu;

    private void OnEnable()
    {
        _gameView.ExitButtonClick += OnCloseButtonClick;
    }

    private void OnDisable()
    {
        _gameView.ExitButtonClick -= OnCloseButtonClick;
    }

    private void OnCloseButtonClick()
    {
        Close();
        OpenedMenu?.Invoke();
    }
}