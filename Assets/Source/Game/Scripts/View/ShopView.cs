using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _nextBoxButton;
    [SerializeField] private Button _previousBoxButton;
    [SerializeField] private Button _buy;

    public event Action ExitButtonClick;
    public event Action NextBoxButtonClick;
    public event Action PreviousBoxButtonClick;

    private void OnEnable()
    {
        _exitButton.onClick.AddListener(OnExitButtonClick);
        _nextBoxButton.onClick.AddListener(OnNextBoxButtonClick);
        _previousBoxButton.onClick.AddListener(OnPreviousBoxButtonClick);
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(OnExitButtonClick);
        _nextBoxButton.onClick.RemoveListener(OnNextBoxButtonClick);
        _nextBoxButton.onClick.RemoveListener(OnPreviousBoxButtonClick);
    }

    private void OnExitButtonClick()
    {
        ExitButtonClick?.Invoke();
    }

    private void OnNextBoxButtonClick()
    {
        NextBoxButtonClick?.Invoke();
    }

    private void OnPreviousBoxButtonClick()
    {
        PreviousBoxButtonClick?.Invoke();
    }
}