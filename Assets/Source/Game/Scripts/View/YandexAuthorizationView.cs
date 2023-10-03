using System;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Game.Scripts.View
{
    public class YandexAuthorizationView : MonoBehaviour
    {
        [SerializeField] private Button _singIn;

        public event Action ClickButton;

        private void OnEnable() =>
            _singIn.onClick.AddListener(OnClickButton);

        private void OnDisable() =>
            _singIn.onClick.RemoveListener(OnClickButton);

        private void OnClickButton()
        {
            if (!PlayerAccount.IsAuthorized)
                ClickButton?.Invoke();
        }

        public void Open()
        {
            const float timeAnimation = 0.2f;

            _singIn.gameObject.LeanScale(Vector3.one, timeAnimation).setEaseInBack();
        }

        public void Close()
        {
            const float timeAnimation = 0.2f;

            _singIn.gameObject.LeanScale(Vector3.zero, timeAnimation).setEaseInBack();
        }
    }
}