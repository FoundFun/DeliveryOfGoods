using UnityEngine;
using DeliveryOfGoods.Model;

public class GameRoot : MonoBehaviour
{
    [SerializeField] private Config _config;
    [SerializeField] private MenuGamePresenter _menuGamePresenter;
    [SerializeField] private GamePresenter _gamePresenter;
    [SerializeField] private SettingsPresenter _settingsPresenter;
    [SerializeField] private TruckPresenter _truckPresenter;
    [SerializeField] private BordSkipPresenter _bordSkipPresenter;
    [SerializeField] private BordHeartPresenter _bordHeartPresenter;
    [SerializeField] private WerehousePresenter _werehousePresenter;
    [SerializeField] private SceneLoader _sceneLoader;

    private void Awake()
    {
        _bordHeartPresenter.Init();
        _gamePresenter.Init();
        _menuGamePresenter.Open();
        _gamePresenter.Close();
        _settingsPresenter.Close();
    }

    private void Start()
    {
        _config.UpdateValue();
        _sceneLoader.Load();
        _werehousePresenter.Reset();
    }

    private void OnEnable()
    {
        _config.ChangedTargetScore += _gamePresenter.SetTargetScore;
        _menuGamePresenter.OpenedGame += _gamePresenter.Open;
        _menuGamePresenter.OpenedSettings += _settingsPresenter.Open;
        _settingsPresenter.OpenedMenu += _menuGamePresenter.Open;
        _gamePresenter.OpenedMenu += _menuGamePresenter.Open;
        _gamePresenter.ResetScene += _truckPresenter.ResetScene;
        _gamePresenter.ResetHeart += _bordHeartPresenter.Reset;
        _gamePresenter.LoadedNextScene += _truckPresenter.PassOnNextLevel;
        _bordSkipPresenter.Restart += _truckPresenter.ResetScene;
        _truckPresenter.AddScoreBody += _gamePresenter.OnAddScore;
        _truckPresenter.LevelCompleted += _gamePresenter.OnLevelCompleted;
        _werehousePresenter.BoxFallen += _gamePresenter.OnBoxFallen;
    }

    private void OnDisable()
    {
        _config.ChangedTargetScore -= _gamePresenter.SetTargetScore;
        _menuGamePresenter.OpenedGame -= _gamePresenter.Open;
        _menuGamePresenter.OpenedSettings -= _settingsPresenter.Open;
        _settingsPresenter.OpenedMenu -= _menuGamePresenter.Open;
        _gamePresenter.OpenedMenu -= _menuGamePresenter.Open;
        _gamePresenter.ResetScene -= _truckPresenter.ResetScene;
        _gamePresenter.ResetHeart -= _bordHeartPresenter.Reset;
        _gamePresenter.LoadedNextScene -= _truckPresenter.PassOnNextLevel;
        _bordSkipPresenter.Restart -= _truckPresenter.ResetScene;
        _truckPresenter.AddScoreBody -= _gamePresenter.OnAddScore;
        _truckPresenter.LevelCompleted -= _gamePresenter.OnLevelCompleted;
        _werehousePresenter.BoxFallen -= _gamePresenter.OnBoxFallen;
    }
}