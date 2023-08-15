using System;
using UnityEngine;

public class ShopPresenter : ScreenPresenter
{
    [SerializeField] private ShopView _shopView;

    public event Action OpenedMenu;

    private void OnEnable()
    {
        _shopView.ExitButtonClick += OnCloseButtonClick;
    }

    private void OnDisable()
    {
        _shopView.ExitButtonClick -= OnCloseButtonClick;
    }

    private void OnCloseButtonClick()
    {
        Close();
        OpenedMenu?.Invoke();
    }
}