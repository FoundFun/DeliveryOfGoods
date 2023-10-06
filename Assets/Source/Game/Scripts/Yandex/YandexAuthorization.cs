using System.Collections;
using UnityEngine;
using Agava.YandexGames;
using Source.Game.Scripts.Spawn;
using Source.Game.Scripts.View;

namespace Source.Game.Scripts.Yandex
{
    public class YandexAuthorization : MonoBehaviour
    {
        [SerializeField] private YandexAuthorizationView _view;
        
        private Coroutine _coroutineAuthorize;
        private SpawnerBox _spawnerBox;

        private void OnEnable() => 
            _view.AcceptButtonClick += Authorize;

        private void OnDisable() => 
            _view.AcceptButtonClick -= Authorize;

        public void Init(SpawnerBox spawnerBox) => 
            _spawnerBox = spawnerBox;

        public void Open()
        {
            _view.Open();
            _spawnerBox.Inactive();
            _spawnerBox.Reset();
        }

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
            OnAuthorizeButtonClick();

            yield return new WaitUntil(() => PlayerAccount.IsAuthorized);

            OnRequestPersonalProfileDataPermissionButtonClick();
            Close();
        }

        private void Close()
        {
            _view.Close();
            _spawnerBox.Active();
        }
    }
}