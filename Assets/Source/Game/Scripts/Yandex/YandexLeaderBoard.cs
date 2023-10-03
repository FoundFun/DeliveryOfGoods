using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Agava.YandexGames;
using Source.Game.Scripts.Configure;
using Source.Game.Scripts.Model;
using Source.Game.Scripts.Spawn;
using Source.Game.Scripts.View;

namespace Source.Game.Scripts.Yandex
{
    public class YandexLeaderBoard : MonoBehaviour
    {
        [SerializeField] private List<LeaderBoardView> _boards;
        [SerializeField] private YandexLeaderBoardView _viewLeaderBoard;
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
            _viewLeaderBoard.IsOpened += Open;
            _viewLeaderBoard.IsClosed += Close;
        }

        private void OnDisable()
        {
            _viewLeaderBoard.IsOpened -= Open;
            _viewLeaderBoard.IsClosed -= Close;
        }

        public void Init(Config config, SpawnerBox spawnerBox)
        {
            _config = config;
            _spawnerBox = spawnerBox;
        }

        public void OnSetLeaderboardScoreButtonClick()
        {
            Leaderboard.GetPlayerEntry("TheBestLevel", (result) =>
            {
                if(result.score < _config.ScoreLeaderBord)
                    Leaderboard.SetScore("TheBestLevel", _config.ScoreLeaderBord);
            });
        }

        private void OnGetLeaderboardEntriesButtonClick()
        {
            Clear();

            Leaderboard.GetEntries("TheBestLevel", (result) =>
            {
                for (int i = 0; i < result.entries.Take(_boards.Count).Count(); i++)
                {
                    string publicName = result.entries[i].player.publicName;
                    int publicScore = result.entries[i].score;

                    if (string.IsNullOrEmpty(publicName))
                    {
                        publicName = publicName switch
                        {
                            YandexInitialize.English => English,
                            YandexInitialize.Russian => Russian,
                            YandexInitialize.Turkish => Turkish,
                            _ => publicName
                        };
                    }

                    SetValueLeaderBord(i, publicScore, publicName);
                }
            });
        }

        private void SetValueLeaderBord(int index, int publicScore, string publicName)
        {
            LeaderBoardModel leaderBoardModel =
                new LeaderBoardModel(_boards[index], index + 1, publicName, publicScore);
            
            leaderBoardModel.SetValue();
        }

        private void Open()
        {
#if YANDEX_GAMES
            if (!PlayerAccount.IsAuthorized)
            {
                AuthorizeOpen();
            }
            else if (!PlayerAccount.HasPersonalProfileDataPermission)
            {
                if (_coroutineErrorBord != null)
                    StopCoroutine(_coroutineErrorBord);

                _coroutineErrorBord = StartCoroutine(ShowErrorBord());
            }
#endif

            if (!PlayerAccount.HasPersonalProfileDataPermission) 
                return; 
            
            OnGetLeaderboardEntriesButtonClick();
            OpenTopListPlayer();
        }

        private void Close()
        {
            const float timeAnimation = 0.5f;

            gameObject.LeanScale(Vector3.zero, timeAnimation).setEaseInBack();

            if (!_config.IsGaming)
                return;

            _spawnerBox.Active();
        }

        private void AuthorizeOpen() => 
            _authorization.Open();

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

            Close();

            _viewErrorBord.LeanScale(Vector3.one, timeOpenAnimation).setEaseOutElastic();

            yield return new WaitForSeconds(timeOpenAnimation);

            _viewErrorBord.LeanScale(Vector3.zero, timeCloseAnimation).setEaseInBack();
        }

        private void Clear()
        {
            foreach (LeaderBoardView bord in _boards)
                bord.Clear();
        }
    }
}