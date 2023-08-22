using UnityEngine;

public class GameRoot : MonoBehaviour
{
    [SerializeField] private MenuGamePresenter _menuGamePresenter;
    [SerializeField] private GamePresenter _gamePresenter;
    [SerializeField] private SettingsPresenter _settingsPresenter;
    [SerializeField] private TruckPresenter _truckPresenter;
    [SerializeField] private BordSkipPresenter _bordSkipPresenter;
    [SerializeField] private BordHeartPresenter _bordHeartPresenter;

    private void Awake()
    {
        _bordHeartPresenter.Init();
    }

    private void OnEnable()
    {
        _menuGamePresenter.OpenedGame += _gamePresenter.Open;
        _menuGamePresenter.OpenedSettings += _settingsPresenter.Open;
        _settingsPresenter.OpenedMenu += _menuGamePresenter.Open;
        _gamePresenter.OpenedMenu += _menuGamePresenter.Open;
        _gamePresenter.ResetScene += _truckPresenter.ResetScene;
        _gamePresenter.ResetHeart += _bordHeartPresenter.Reset;
        _bordSkipPresenter.Restart += _truckPresenter.ResetScene;
    }

    private void OnDisable()
    {
        _menuGamePresenter.OpenedGame -= _gamePresenter.Open;
        _menuGamePresenter.OpenedSettings -= _settingsPresenter.Open;
        _settingsPresenter.OpenedMenu -= _menuGamePresenter.Open;
        _gamePresenter.OpenedMenu -= _menuGamePresenter.Open;
        _gamePresenter.ResetScene -= _truckPresenter.ResetScene;
        _gamePresenter.ResetHeart -= _bordHeartPresenter.Reset;
        _bordSkipPresenter.Restart -= _truckPresenter.ResetScene;
    }
}