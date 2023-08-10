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
    }

    private void OnDisable()
    {
        _shopView.ExitButtonClick -= OnCloseButtonClick;
        _shopView.UpgradedConveyorButtonClick -= OnUpgradeConveyor;
        _shopView.UpgradedSpawnButtonClick -= OnUpgradeSpawn;
    }


    private void OnUpgradeConveyor()
    {

    }

    private void OnUpgradeSpawn()
    {
        
    }

    private void OnCloseButtonClick()
    {
        Close();
        OpenedMenu?.Invoke();
    }
}