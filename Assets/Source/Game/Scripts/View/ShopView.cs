using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _UpgradeConveyor;
    [SerializeField] private Button _UpgradeSpawn;
    [SerializeField] private Button _UpgradeNumberBoxs;

    public event Action ExitButtonClick;
    public event Action UpgradedConveyorButtonClick;
    public event Action UpgradedSpawnButtonClick;
    public event Action UpgradedNumberBoxsButtonClick;

    private void OnEnable()
    {
        _exitButton.onClick.AddListener(OnExitButtonClick);
        _UpgradeConveyor.onClick.AddListener(OnUpgradedConveyorButtonClick);
        _UpgradeSpawn.onClick.AddListener(OnUpgradedSpawnButtonClick);
        _UpgradeNumberBoxs.onClick.AddListener(OnUpgradedNumberBoxsButtonClick);
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(OnExitButtonClick);
        _UpgradeConveyor.onClick.RemoveListener(OnUpgradedConveyorButtonClick);
        _UpgradeSpawn.onClick.RemoveListener(OnUpgradedSpawnButtonClick);
        _UpgradeNumberBoxs.onClick.RemoveListener(OnUpgradedNumberBoxsButtonClick);
    }

    private void OnUpgradedConveyorButtonClick()
    {
        UpgradedConveyorButtonClick?.Invoke();
    }

    private void OnUpgradedSpawnButtonClick()
    {
        UpgradedSpawnButtonClick?.Invoke();
    }

    private void OnUpgradedNumberBoxsButtonClick()
    {
        UpgradedNumberBoxsButtonClick?.Invoke();
    }

    private void OnExitButtonClick()
    {
        ExitButtonClick?.Invoke();
    }
}