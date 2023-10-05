using System.Collections;
using Agava.YandexGames;
using Lean.Localization;
using UnityEngine;

namespace Source.Game.Scripts.Yandex
{
    public class YandexInitialize : MonoBehaviour
    {
        [SerializeField] private LeanLocalization _localization;

        public const string English = "English";
        public const string Russian = "Russian";
        public const string Turkish = "Turkish";
        
        private Coroutine _coroutine;
    
        private void Awake()
        {
            YandexGamesSdk.CallbackLogging = true;
            PlayerAccount.AuthorizedInBackground += OnAuthorizedInBackground;
        }

        private IEnumerator Start()
        {
#if !YANDEX_GAMES
            yield break;
#endif
            if (_coroutine != null)
                StopCoroutine(_coroutine);
        
            _coroutine = StartCoroutine(Init());
        
            yield return YandexGamesSdk.Initialize();
        }
        
        private void OnDestroy() => 
            PlayerAccount.AuthorizedInBackground -= OnAuthorizedInBackground;

        private IEnumerator Init()
        {
            const string enCulture = "en";
            const string ruCulture = "ru";
            const string trCulture = "tr";

            yield return new WaitUntil(() => YandexGamesSdk.IsInitialized);
        
            string localization = YandexGamesSdk.Environment.i18n.lang;

            localization = localization switch
            {
                enCulture => English,
                ruCulture => Russian,
                trCulture => Turkish,
                _ => localization
            };

            _localization.SetCurrentLanguage(localization);
        }
        
        private void OnAuthorizedInBackground() => 
            Debug.Log($"{nameof(OnAuthorizedInBackground)} {PlayerAccount.IsAuthorized}");
    }
}
