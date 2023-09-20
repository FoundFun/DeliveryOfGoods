using System.Collections;
using Agava.YandexGames;
using Lean.Localization;
using UnityEngine;

namespace Source.Game.Scripts
{
    public class YandexInitialize : MonoBehaviour
    {
        [SerializeField] private YandexShowAds _yandexShowAds;
        [SerializeField] private LeanLocalization _localization;
    
        private Coroutine _coroutine;
    
        private void Awake()
        {
            YandexGamesSdk.CallbackLogging = true;
        }

        private IEnumerator Start()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
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
        
            _yandexShowAds.OnShowInterstitialButtonClick();

            string localization = YandexGamesSdk.Environment.i18n.lang;

            switch (localization)
            {
                case enCulture:
                    localization = english;
                    break;
                case ruCulture:
                    localization = russian;
                    break;
                case trCulture:
                    localization = turkish;
                    break;
            }
        
            _localization.SetCurrentLanguage(localization);
        }
    }
}
