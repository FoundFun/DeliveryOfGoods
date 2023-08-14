using DeliveryOfGoods.Model;
using System;
using UnityEngine;

public class ShopPresenter : ScreenPresenter
{
    [SerializeField] private ShopView _shopView;

    public event Action OpenedMenu;

    private void OnEnable()
    {
        _shopView.ExitButtonClick += OnCloseButtonClick;
        _shopView.UpgradedConveyorButtonClick += OnUpgradeConveyor;
        _shopView.UpgradedSpawnButtonClick += OnUpgradeSpawn;
        _shopView.UpgradedNumberBoxsButtonClick += OnUpgradeNumberBoxs;
    }

    private void OnDisable()
    {
        _shopView.ExitButtonClick -= OnCloseButtonClick;
        _shopView.UpgradedConveyorButtonClick -= OnUpgradeConveyor;
        _shopView.UpgradedSpawnButtonClick -= OnUpgradeSpawn;
    }

    private void OnUpgradeConveyor()
    {
        Config.UpgradeConveyor();
    }

    private void OnUpgradeSpawn()
    {
        Config.UpgradeSpawn();
    }

    private void OnUpgradeNumberBoxs()
    {
        Config.UpgradeNumberBoxs();
    }

    private void OnCloseButtonClick()
    {
        Close();
        OpenedMenu?.Invoke();
    }
}