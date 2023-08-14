using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _UpgradeConveyor;
    [SerializeField] private Button _UpgradeSpawn;
    [SerializeField] private Button _UpgradeNumberBoxs;
    [SerializeField] private WalletPresenter _wallet;

    private string _priceConveyor;
    private string _priceSpawn;
    private string _priceNumberBoxs;

    public event Action ExitButtonClick;
    public event Action UpgradedConveyorButtonClick;
    public event Action UpgradedSpawnButtonClick;
    public event Action UpgradedNumberBoxsButtonClick;

    private void Start()
    {
        _priceConveyor = _UpgradeConveyor.GetComponentInChildren<TMP_Text>().text;
        _priceSpawn = _UpgradeSpawn.GetComponentInChildren<TMP_Text>().text;
        _priceNumberBoxs = _UpgradeNumberBoxs.GetComponentInChildren<TMP_Text>().text;
    }

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
        int price = int.Parse(_priceConveyor);

        if (_wallet.Money >= price)
        {
            UpgradedConveyorButtonClick?.Invoke();
            _wallet.RemoveMoney(price);
        }
    }

    private void OnUpgradedSpawnButtonClick()
    {
        int price = int.Parse(_priceSpawn);

        if (_wallet.Money >= price)
        {
            UpgradedSpawnButtonClick?.Invoke();
            _wallet.RemoveMoney(price);
        }
    }

    private void OnUpgradedNumberBoxsButtonClick()
    {
        int price = int.Parse(_priceNumberBoxs);

        if (_wallet.Money >= price)
        {
            UpgradedNumberBoxsButtonClick?.Invoke();
            _wallet.RemoveMoney(price);
        }
    }

    private void OnExitButtonClick()
    {
        ExitButtonClick?.Invoke();
    }
}