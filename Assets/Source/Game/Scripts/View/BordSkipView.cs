using System;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Game.Scripts.View
{
    public class BordSkipView : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;

        public event Action Restart;

        private void OnEnable() => 
            _restartButton.onClick.AddListener(OnRestart);

        private void OnDisable() => 
            _restartButton.onClick.RemoveListener(OnRestart);

        private void OnRestart() => 
            Restart?.Invoke();
    }
}
