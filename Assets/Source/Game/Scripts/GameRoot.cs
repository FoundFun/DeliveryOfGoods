using UnityEngine;
using DeliveryOfGoods.Model;

public class GameRoot : MonoBehaviour
{
    [SerializeField] private BeltPresenter[] _beltsPresenter;
    [SerializeField] private BoxPresenter[] _boxsPresenter;
    [SerializeField] private MenuGamePresenter _menuGamePresenter;
    [SerializeField] private GamePresenter _gamePresenter;
    [SerializeField] private ShopPresenter _shopPresenter;
    [SerializeField] private SettingsPresenter _settingsPresenter;

    private void Awake()
    {
        foreach (var beltPresenter in _beltsPresenter)
        {
            Belt belt = new Belt();
            beltPresenter.Init(belt);
        }
    }

    private void Start()
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