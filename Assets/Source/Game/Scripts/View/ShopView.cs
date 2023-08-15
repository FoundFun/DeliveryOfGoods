using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _upgradeConveyor;
    [SerializeField] private Button _upgradeSpawn;
    [SerializeField] private Button _upgradeNumberBoxs;
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
        _priceConveyor = _upgradeConveyor.GetComponentInChildren<TMP_Text>().text;
        _priceSpawn = _upgradeSpawn.GetComponentInChildren<TMP_Text>().text;
        _priceNumberBoxs = _upgradeNumberBoxs.GetComponentInChildren<TMP_Text>().text;
    }

    private void OnEnable()
    {
        _exitButton.onClick.AddListener(OnExitButtonClick);
        _upgradeConveyor.onClick.AddListener(OnUpgradedConveyorButtonClick);
        _upgradeSpawn.onClick.AddListener(OnUpgradedSpawnButtonClick);
        _upgradeNumberBoxs.onClick.AddListener(OnUpgradedNumberBoxsButtonClick);
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(OnExitButtonClick);
        _upgradeConveyor.onClick.RemoveListener(OnUpgradedConveyorButtonClick);
        _upgradeSpawn.onClick.RemoveListener(OnUpgradedSpawnButtonClick);
        _upgradeNumberBoxs.onClick.RemoveListener(OnUpgradedNumberBoxsButtonClick);
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