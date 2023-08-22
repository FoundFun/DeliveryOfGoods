using System;
using UnityEngine;

public class GamePresenter : ScreenPresenter
{
    [SerializeField] private GameView _gameView;
    [SerializeField] private SpawnerBox _spawnerBox;
    [SerializeField] private WerehousePresenter _werehousePresenter;
    [SerializeField] private BordHeartPresenter _bordHeart;

    public event Action OpenedMenu;
    public event Action ResetScene;
    public event Action ResetHeart;

    private void OnEnable()
    {
        _gameView.ExitButtonClick += OnCloseButtonClick;
        _werehousePresenter.BoxFallen += OnBoxFallen;
    }

    private void OnDisable()
    {
        _gameView.ExitButtonClick -= OnCloseButtonClick;
        _werehousePresenter.BoxFallen -= OnBoxFallen;
    }

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
    }

    private void OnCloseButtonClick()
    {
        Close();
        OpenedMenu?.Invoke();
    }

    private void OnBoxFallen()
    {
        _bordHeart.TakeDamage();
    }
}