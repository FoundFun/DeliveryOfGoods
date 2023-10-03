using System.Collections;
using UnityEngine;
using Agava.YandexGames;
using Source.Game.Scripts.View;

namespace Source.Game.Scripts.Yandex
{
    public class YandexAuthorization : MonoBehaviour
    {
        [SerializeField] private YandexAuthorizationView _view;
        
        private Coroutine _coroutineAuthorize;

        private void OnEnable() => 
            _view.ClickButton += Authorize;

        private void OnDisable() => 
            _view.ClickButton -= Authorize;

        public void Open() => 
            _view.Open();

        private void OnAuthorizeButtonClick() =>
            PlayerAccount.Authorize();

        private void OnRequestPersonalProfileDataPermissionButtonClick() =>
            PlayerAccount.RequestPersonalProfileDataPermission();

        private void Authorize()
        {
#if YANDEX_GAMES
            if (_coroutineAuthorize != null)
                StopCoroutine(_coroutineAuthorize);

            _coroutineAuthorize = StartCoroutine(OnAuthorize());
#endif
        }

        private IEnumerator OnAuthorize()
        {
            Close();
            OnAuthorizeButtonClick();

            yield return new WaitUntil(() => PlayerAccount.IsAuthorized);

            OnRequestPersonalProfileDataPermissionButtonClick();
        }

        private void Close() => 
            _view.Close();
    }
}