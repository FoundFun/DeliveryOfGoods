using System.Collections;
using Agava.YandexGames;
using Source.Game.Scripts.Configure;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source.Game.Scripts
{
    public class SceneLoader : MonoBehaviour
    {
        private Config _config;

        public void Init(Config config) =>
            _config = config;

        public void Load() =>
            SceneManager.LoadScene(Config.NameScene + _config.CurrentLevel);

        public void InitGameReady() =>
            StartCoroutine(GameReady());

        private IEnumerator GameReady()
        {
#if YANDEX_GAMES
            AsyncOperation scene = SceneManager.LoadSceneAsync(Config.NameScene + _config.CurrentLevel);

            yield return new WaitUntil(() => scene.isDone);
            yield return new WaitUntil(() => YandexGamesSdk.IsInitialized);

            YandexGamesSdk.GameReady();
#else
            Load();
#endif
        }
    }
}