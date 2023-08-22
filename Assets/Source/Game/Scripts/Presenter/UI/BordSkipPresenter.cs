using System;
using UnityEngine;

public class BordSkipPresenter : MonoBehaviour
{
    [SerializeField] private BordSkipView _view;
    [SerializeField] private BordHeartPresenter _heartPresenter;

    public event Action Restart;

    private void OnEnable()
    {
        _view.Restart += OnRestart;
    }

    private void OnDisable()
    {
        _view.Restart -= OnRestart;
    }

    public void OnRestart()
    {
        Restart?.Invoke();
        _heartPresenter.Recover();
    }
}