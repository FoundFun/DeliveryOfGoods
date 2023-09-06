using System;
using UnityEngine;
using UnityEngine.UI;

public class BordResurrectView : MonoBehaviour
{
    [SerializeField] private Button _showAdsButton;
    
    public event Action ShowAds;
    
    private void OnEnable()
    {
        _showAdsButton.onClick.AddListener(OnShowAds);
    }

    private void OnDisable()
    {
        _showAdsButton.onClick.RemoveListener(OnShowAds);
    }

    private void OnShowAds()
    {
        ShowAds?.Invoke();
    }
}
