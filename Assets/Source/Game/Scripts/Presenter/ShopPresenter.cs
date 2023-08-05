using DeliveryOfGoods.Model;
using System;
using UnityEngine;

public class ShopPresenter : ScreenPresenter
{
    [SerializeField] private ShopView _shopView;
    [SerializeField] private BoxPresenter[] _boxes;

    private Shop _shop;
    private int _currentBoxIndex;

    public event Action OpenedMenu;

    private void OnEnable()
    {
        _shopView.ExitButtonClick += OnCloseButtonClick;
        _shopView.NextBoxButtonClick += ShowNext;
        _shopView.PreviousBoxButtonClick += ShowPrevios;
    }

    private void OnDisable()
    {
        _shopView.ExitButtonClick -= OnCloseButtonClick;
        _shopView.NextBoxButtonClick -= ShowNext;
        _shopView.PreviousBoxButtonClick -= ShowPrevios;
    }

    public void Init(Shop shop)
    {
        _shop = shop;

        foreach (var box in _boxes)
            _shop.Clean(box);
    }

    public void ShowNext()
    {
        if (_currentBoxIndex < _boxes.Length - 1)
        {
            _currentBoxIndex++;

            if (_currentBoxIndex > 0)
            {
                _shop.Clean(_boxes[_currentBoxIndex - 1]);
            }
        }

        _shop.Show(_boxes[_currentBoxIndex]);
    }

    public void ShowPrevios()
    {
        if (_currentBoxIndex > 0)
        {
            _currentBoxIndex--;

            if (_currentBoxIndex < _boxes.Length - 1)
            {
                _shop.Clean(_boxes[_currentBoxIndex + 1]);
            }
        }

        _shop.Show(_boxes[_currentBoxIndex]);
    }

    private void OnCloseButtonClick()
    {
        Close();
        OpenedMenu?.Invoke();
    }
}