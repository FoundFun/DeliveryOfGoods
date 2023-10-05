using System;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Game.Scripts.View
{
    public class BoardResurrectView : MonoBehaviour
    {
        [SerializeField] private Button _showAdsButton;
    
        public event Action ShowAds;
    
        private void OnEnable() => 
            _showAdsButton.onClick.AddListener(OnShowAds);

        private void OnDisable() => 
            _showAdsButton.onClick.RemoveListener(OnShowAds);
        
        public void ActiveRestartButton() => 
            _showAdsButton.interactable = true;

        public void InactiveRestartButton() => 
            _showAdsButton.interactable = false;

        private void OnShowAds() => 
            ShowAds?.Invoke();
    }
}
