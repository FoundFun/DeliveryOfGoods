using System;
using UnityEngine;

public class BordSkipPresenter : MonoBehaviour
{
    [SerializeField] private BordSkipView _view;
    [SerializeField] private BordHeartPresenter _heartPresenter;
    [SerializeField] private YandexShowAds _yandexShowAds;

    public event Action Restart;

    private void OnEnable()
    {
        _view.Restart += OnRestart;
    }

    private void OnDisable()
    {
        _view.Restart -= OnRestart;
    }

    private void OnRestart()
    {
#if !UNITY_EDITOR
        _yandexShowAds.OnShowInterstitialButtonClick();
#endif
        Restart?.Invoke();
        _heartPresenter.Recover();
    }
}