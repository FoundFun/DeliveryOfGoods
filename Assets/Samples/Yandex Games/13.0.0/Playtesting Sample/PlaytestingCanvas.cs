#pragma warning disable

using System.Collections;
using Agava.YandexGames;
using Agava.YandexGames.Samples;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DeliveryOfGoods.Model;
using UnityEngine.SceneManagement;

namespace Agava.YandexGames.Samples
{
    public class PlaytestingCanvas : MonoBehaviour
    {
        [SerializeField] private AudioSource _gameMusic;
        private void Awake()
        {
            YandexGamesSdk.CallbackLogging = true;
        }

        private IEnumerator Start()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            yield break;
#endif

            // Always wait for it if invoking something immediately in the first scene.
            yield return YandexGamesSdk.Initialize();
        }

        public void OnShowInterstitialButtonClick()
        {
            InterstitialAd.Show(StopGame, StartGame);
        }

        public void OnShowVideoButtonClick()
        {
            VideoAd.Show();
        }

        public void OnShowStickyAdButtonClick()
        {
            StickyAd.Show();
        }

        public void OnHideStickyAdButtonClick()
        {
            StickyAd.Hide();
        }

        public void OnAuthorizeButtonClick()
        {
            PlayerAccount.Authorize();
        }

        public void OnRequestPersonalProfileDataPermissionButtonClick()
        {
            PlayerAccount.RequestPersonalProfileDataPermission();
        }

        public void OnGetProfileDataButtonClick()
        {
            PlayerAccount.GetProfileData((result) =>
            {
                string name = result.publicName;
                if (string.IsNullOrEmpty(name))
                    name = "Anonymous";
                Debug.Log($"My id = {result.uniqueID}, name = {name}");
            });
        }

        public void OnSetLeaderboardScoreButtonClick()
        {
            Leaderboard.SetScore("PlaytestBoard", Random.Range(1, 100));
        }

        public void OnGetLeaderboardEntriesButtonClick()
        {
            Leaderboard.GetEntries("PlaytestBoard", (result) =>
            {
                Debug.Log($"My rank = {result.userRank}");
                foreach (var entry in result.entries)
                {
                    string name = entry.player.publicName;
                    if (string.IsNullOrEmpty(name))
                        name = "Anonymous";
                    Debug.Log(name + " " + entry.score);
                }
            });
        }

        public void OnGetLeaderboardPlayerEntryButtonClick()
        {
            Leaderboard.GetPlayerEntry("PlaytestBoard", (result) =>
            {
                if (result == null)
                    Debug.Log("Player is not present in the leaderboard.");
                else
                    Debug.Log($"My rank = {result.rank}, score = {result.score}");
            });
        }

        public void OnSetCloudSaveDataButtonClick()
        {
            //PlayerAccount.SetCloudSaveData(Config.CurrentLevel.ToString());
        }

        public void OnGetCloudSaveDataButtonClick()
        {
            //PlayerAccount.GetCloudSaveData((data) => Config.GetValueCloud(data));
        }

        public void OnGetEnvironmentButtonClick()
        {
            Debug.Log($"Environment = {JsonUtility.ToJson(YandexGamesSdk.Environment)}");
        }

        private void StartGame(bool wasShow)
        {
            if (wasShow)
            {
                Time.timeScale = 1;
                _gameMusic.mute = false;
            }
        }

        private void StopGame()
        {
            Time.timeScale = 0;
            _gameMusic.mute = true;
        }
    }
}
