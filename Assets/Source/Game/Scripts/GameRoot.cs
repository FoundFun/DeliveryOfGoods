using UnityEngine;
using DeliveryOfGoods.Model;

public class GameRoot : MonoBehaviour
{
    [SerializeField] private MenuGamePresenter _menuGamePresenter;
    [SerializeField] private GamePresenter _gamePresenter;
    [SerializeField] private ShopPresenter _shopPresenter;
    [SerializeField] private SettingsPresenter _settingsPresenter;

    private void Awake()
    {
        Shop shop = new Shop();
        _shopPresenter.Init(shop);
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