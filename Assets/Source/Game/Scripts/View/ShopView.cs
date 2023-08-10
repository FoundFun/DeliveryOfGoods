using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _UpgradeConveyor;
    [SerializeField] private Button _UpgradeSpawn;

    public event Action ExitButtonClick;
    public event Action UpgradedConveyorButtonClick;
    public event Action UpgradedSpawnButtonClick;

    private void OnEnable()
    {
        _exitButton.onClick.AddListener(OnExitButtonClick);
        _UpgradeConveyor.onClick.AddListener(OnUpgradedConveyorButtonClick);
        _UpgradeSpawn.onClick.AddListener(OnUpgradedSpawnButtonClick);
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(OnExitButtonClick);
        _UpgradeConveyor.onClick.RemoveListener(OnUpgradedConveyorButtonClick);
        _UpgradeSpawn.onClick.RemoveListener(OnUpgradedSpawnButtonClick);
    }

    private void OnUpgradedConveyorButtonClick()
    {
        UpgradedConveyorButtonClick?.Invoke();
    }

    private void OnUpgradedSpawnButtonClick()
    {
        UpgradedSpawnButtonClick?.Invoke();
    }

    private void OnExitButtonClick()
    {
        ExitButtonClick?.Invoke();
    }
}