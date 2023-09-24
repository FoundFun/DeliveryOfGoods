using System;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Game.Scripts.View
{
    public class MenuGameView : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _settingsButton;

        public event Action StartButtonClick;
        public event Action SettingsButtonClick;

        private void OnEnable()
        {
            _startButton.onClick.AddListener(OnStartButtonClick);
            _settingsButton.onClick.AddListener(OnSettingsButtonClick);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(OnStartButtonClick);
            _settingsButton.onClick.RemoveListener(OnSettingsButtonClick);
        }

        private void OnStartButtonClick()
        {
            StartButtonClick?.Invoke();
        }

        private void OnSettingsButtonClick()
        {
            SettingsButtonClick?.Invoke();
        }
    }
}