using UnityEngine;

public class GameRoot : MonoBehaviour
{
    [SerializeField] private MenuGamePresenter _menuGamePresenter;
    [SerializeField] private GamePresenter _gamePresenter;
    [SerializeField] private ShopPresenter _shopPresenter;
    [SerializeField] private SettingsPresenter _settingsPresenter;
    [SerializeField] private WalletPresenter _playerPresenter;

    private void Start()
    {
        _playerPresenter.Init();
    }

    private void OnEnable()
    {
        _menuGamePresenter.OpenedGame += _gamePresenter.Open;
        _menuGamePresenter.OpenedSettings += _settingsPresenter.Open;
        _menuGamePresenter.OpenedShop += _shopPresenter.Open;
        _settingsPresenter.OpenedMenu += _menuGamePresenter.Open;
        _shopPresenter.OpenedMenu += _menuGamePresenter.Open;
        _gamePresenter.OpenedMenu += _menuGamePresenter.Open;
    }

    private void OnDisable()
    {
        _menuGamePresenter.OpenedGame -= _gamePresenter.Open;
        _menuGamePresenter.OpenedSettings -= _settingsPresenter.Open;
        _menuGamePresenter.OpenedShop -= _shopPresenter.Open;
        _settingsPresenter.OpenedMenu -= _menuGamePresenter.Open;
        _shopPresenter.OpenedMenu -= _menuGamePresenter.Open;
        _gamePresenter.OpenedMenu -= _menuGamePresenter.Open;
    }
}