using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuGameView : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _adsReward;
    [SerializeField] private Button _settingsButton;

    public event Action StartButtonClick;
    public event Action ShopButtonClick;
    public event Action SettingsButtonClick;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(OnStartButtonClick);
        _shopButton.onClick.AddListener(OnShopButtonClick);
        _settingsButton.onClick.AddListener(OnSettingsButtonClick);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(OnStartButtonClick);
        _shopButton.onClick.RemoveListener(OnShopButtonClick);
        _settingsButton.onClick.RemoveListener(OnSettingsButtonClick);
    }

    private void OnStartButtonClick()
    {
        StartButtonClick?.Invoke();
    }

    private void OnShopButtonClick()
    {
        ShopButtonClick?.Invoke();
    }

    private void OnSettingsButtonClick()
    {
        SettingsButtonClick?.Invoke();
    }
}