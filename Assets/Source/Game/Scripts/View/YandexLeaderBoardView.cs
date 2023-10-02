using System;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Game.Scripts.View
{
    public class YandexLeaderBoardView : MonoBehaviour
    {
        [SerializeField] private Button _openButton;
        [SerializeField] private Button _closeButton;

        public event Action IsOpened;
        public event Action IsClosed;

        private void OnEnable()
        {
            _openButton.onClick.AddListener(OnOpen);
            _closeButton.onClick.AddListener(OnClose);
        }

        private void OnDisable()
        {
            _openButton.onClick.RemoveListener(OnOpen);
            _closeButton.onClick.RemoveListener(OnClose);
        }
        
        private void OnOpen() => 
            IsOpened?.Invoke();

        private void OnClose() => 
            IsClosed?.Invoke();
    }
}
