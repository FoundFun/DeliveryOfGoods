using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Agava.YandexGames;
using Source.Game.Scripts.Configure;
using Source.Game.Scripts.Spawn;
using Source.Game.Scripts.View;

namespace Source.Game.Scripts.Yandex
{
    public class YandexLeaderBord : MonoBehaviour
    {
        [SerializeField] private List<BordView> _bords;
        [SerializeField] private YandexLeaderBordView _viewLeaderBord;
        [SerializeField] private GameObject _viewErrorBord;
        [SerializeField] private YandexAuthorization _authorization;

        private const string English = "Anonymous";
        private const string Russian = "Аноним";
        private const string Turkish = "Anonim";

        private Config _config;
        private SpawnerBox _spawnerBox;
        private Coroutine _coroutineAuthorize;
        private Coroutine _coroutineErrorBord;

        private void OnEnable()
        {
            _viewLeaderBord.IsOpened += Open;
            _viewLeaderBord.IsClosed += Close;
        }

        private void OnDisable()
        {
            _viewLeaderBord.IsOpened -= Open;
            _viewLeaderBord.IsClosed -= Close;
        }

        public void Init(Config config, SpawnerBox spawnerBox)
        {
            _config = config;
            _spawnerBox = spawnerBox;
        }

        public void OnGetProfileDataButtonClick()
        {
            PlayerAccount.GetProfileData((result) =>
            {
                string name = result.publicName;

                if (string.IsNullOrEmpty(name))
                {
                    name = name switch
                    {
                        YandexInitialize.English => English,
                        YandexInitialize.Russian => Russian,
                        YandexInitialize.Turkish => Turkish,
                        _ => name
                    };
                }

                Debug.Log($"My id = {result.uniqueID}, name = {name}");
            });
        }

        public void OnSetLeaderboardScoreButtonClick() =>
            Leaderboard.SetScore("TheBestLevel", _config.ScoreLeaderBord);

        private void Open()
        {
#if YANDEX_GAMES   
            if (_coroutineAuthorize != null)
                StopCoroutine(_coroutineAuthorize);

            _coroutineAuthorize = StartCoroutine(Authorize());
#endif
            if(_coroutineErrorBord != null)
                StopCoroutine(_coroutineErrorBord);

            _coroutineErrorBord = StartCoroutine(ShowErrorBord());
        }

        private void OnGetLeaderboardEntriesButtonClick()
        {
            Leaderboard.GetEntries("TheBestLevel", (result) =>
            {
                for (int i = 0; i < result.entries.Take(_bords.Count).Count(); i++)
                {
                    string name = result.entries[i].player.publicName;
                    
                    if (string.IsNullOrEmpty(name))
                    {
                        name = name switch
                        {
                            YandexInitialize.English => English,
                            YandexInitialize.Russian => Russian,
                            YandexInitialize.Turkish => Turkish,
                            _ => name
                        };
                    }

                    Bord bord = new Bord(_bords[i], i + 1, name, _config.ScoreLeaderBord);
                    bord.SetValue();
                }
            });
        }

        public void OnGetLeaderboardPlayerEntryButtonClick()
        {
            Leaderboard.GetPlayerEntry("TheBestLevel", (result) =>
            {
                if (result == null)
                    Debug.Log("Player is not present in the leaderboard.");
                else
                    Debug.Log($"My rank = {result.rank}, score = {result.score}");
            });
        }

        private void Close()
        {
            const float timeAnimation = 0.5f;

            gameObject.LeanScale(Vector3.zero, timeAnimation).setEaseInBack();

            if (!_config.IsGaming)
                return;

            _spawnerBox.Active();
        }

        private IEnumerator Authorize()
        {
            if (!PlayerAccount.IsAuthorized)
                _authorization.OnAuthorizeButtonClick();

            yield return new WaitUntil(() => PlayerAccount.IsAuthorized);

            _authorization.OnRequestPersonalProfileDataPermissionButtonClick();

            yield return new WaitUntil(() => PlayerAccount.HasPersonalProfileDataPermission);
            
            OnGetLeaderboardEntriesButtonClick();
            OpenTopListPlayer();
        }

        private void OpenTopListPlayer()
        {
            const float timeAnimation = 1f;
            
            gameObject.LeanScale(Vector3.one, timeAnimation).setEaseOutElastic();

            if (!_config.IsGaming)
                return;

            _spawnerBox.Inactive();
            _spawnerBox.Reset();
        }

        private IEnumerator ShowErrorBord()
        {
            const float timeOpenAnimation = 1f;
            const float timeCloseAnimation = 0.5f;
            
            _viewErrorBord.LeanScale(Vector3.one, timeOpenAnimation).setEaseOutElastic();

            yield return new WaitForSeconds(timeOpenAnimation);

            _viewErrorBord.LeanScale(Vector3.zero, timeCloseAnimation).setEaseInBack();
        }
    }
}