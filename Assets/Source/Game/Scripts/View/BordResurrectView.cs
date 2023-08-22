using System;
using UnityEngine;
using UnityEngine.UI;

public class BordResurrectView : MonoBehaviour
{
    [SerializeField] private Button _resurrectButton;

    public event Action Resurrected;

    private void OnEnable()
    {
        _resurrectButton.onClick.AddListener(OnResurrect);
    }

    private void OnDisable()
    {
        _resurrectButton.onClick.RemoveListener(OnResurrect);
    }

    private void OnResurrect()
    {
        Resurrected?.Invoke();
    }
}
