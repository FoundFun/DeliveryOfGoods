using System;
using UnityEngine;
using DeliveryOfGoods.Model;

public class GamePresenter : ScreenPresenter
{
    [SerializeField] private GameView _gameView;
    [SerializeField] private SpawnerBox _spawnerBox;
    [SerializeField] private BordHeartPresenter _bordHeart;
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private Config _config;
    [SerializeField] private YandexShowAds _yandexShowAds;

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

    public void Init()
    {
        _gameView.Init();
    }

    public void OnAddScore(int score)
    {
        _gameView.AddScore(score);
    }

    public void SetTargetScore(int score)
    {
        _gameView.SetTargetScore(score);
    }

    public void OnBoxFallen()
    {
        _bordHeart.TakeDamage();
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
#if !UNITY_EDITOR
        _yandexShowAds.OnShowInterstitialButtonClick();
#endif
        Close();
        OpenedMenu?.Invoke();
    }

    public void OnLevelCompleted()
    {
        _bordHeart.Reset();
        _gameView.EnableNextButton();
        _spawnerBox.Inactive();
        _spawnerBox.Reset();
    }
    
    private void OnLoadNextLevel()
    {
#if !UNITY_EDITOR
        _yandexShowAds.OnShowInterstitialButtonClick();
#endif
        _config.Improve();
        _sceneLoader.Load();
        _gameView.DisableNextButton();
        LoadedNextScene?.Invoke();
        _spawnerBox.Active();
    }
}