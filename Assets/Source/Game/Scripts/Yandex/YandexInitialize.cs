using System.Collections;
using Agava.YandexGames;
using Lean.Localization;
using UnityEngine;

namespace Source.Game.Scripts.Yandex
{
    public class YandexInitialize : MonoBehaviour
    {
        [SerializeField] private YandexShowAds _yandexShowAds;
        [SerializeField] private LeanLocalization _localization;
    
        private Coroutine _coroutine;
    
        private void Awake() => 
            YandexGamesSdk.CallbackLogging = true;

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

        private IEnumerator Init()
        {
            const string enCulture = "en";
            const string ruCulture = "ru";
            const string trCulture = "tr";
            const string english = "English";
            const string russian = "Russian";
            const string turkish = "Turkish";

            yield return new WaitUntil(() => YandexGamesSdk.IsInitialized);
        
            string localization = YandexGamesSdk.Environment.i18n.lang;

            localization = localization switch
            {
                enCulture => english,
                ruCulture => russian,
                trCulture => turkish,
                _ => localization
            };

            _localization.SetCurrentLanguage(localization);
        }
    }
}
