using DeliveryOfGoods.Model;
using System;
using UnityEngine;

public class ShopPresenter : ScreenPresenter
{
    [SerializeField] private ShopView _shopView;

    private Shop _shop;

    public event Action OpenedMenu;

    private void OnEnable()
    {
        _shopView.ExitButtonClick += OnCloseButtonClick;
    }

    private void OnDisable()
    {
        _shopView.ExitButtonClick -= OnCloseButtonClick;
    }

    public void Init(Shop shop)
    {
        _shop = shop;
    }

    private void OnCloseButtonClick()
    {
        Close();
        OpenedMenu?.Invoke();
    }
}