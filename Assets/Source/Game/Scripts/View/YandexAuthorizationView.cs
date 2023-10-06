using System;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Game.Scripts.View
{
    public class YandexAuthorizationView : MonoBehaviour
    {
        [SerializeField] private Image _bordAuthorization;
        [SerializeField] private Button _acceptButton;
        [SerializeField] private Button _rejectButton;

        public event Action AcceptButtonClick;

        private void OnEnable()
        {
            _acceptButton.onClick.AddListener(OnAcceptButtonClick);
            _rejectButton.onClick.AddListener(Close);
        }

        private void OnDisable()
        {
            _acceptButton.onClick.RemoveListener(OnAcceptButtonClick);
            _rejectButton.onClick.RemoveListener(Close);
        }

        private void OnAcceptButtonClick()
        {
            if (!PlayerAccount.IsAuthorized)
                AcceptButtonClick?.Invoke();
        }

        public void Open()
        {
            const float timeAnimation = 0.2f;

            _bordAuthorization.gameObject.LeanScale(Vector3.one, timeAnimation).setEaseInBack();
            _bordAuthorization.raycastTarget = true;
            _acceptButton.interactable = true;
            _rejectButton.interactable = true;
        }

        public void Close()
        {
            const float timeAnimation = 0.2f;

            _bordAuthorization.gameObject.LeanScale(Vector3.zero, timeAnimation).setEaseInBack();
            _bordAuthorization.raycastTarget = false;
            _acceptButton.interactable = false;
            _rejectButton.interactable = false;
        }
    }
}