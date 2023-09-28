using UnityEngine;
using Agava.YandexGames;

namespace Source.Game.Scripts.Yandex
{
    public class YandexLeaderBord : MonoBehaviour
    {
        [SerializeField] private YandexInitialize _initialize;
        
        private const string English = "Anonymous";
        private const string Russian = "Аноним";
        private const string Turkish = "Anonim";

        public void OnAuthorizeButtonClick() => 
            PlayerAccount.Authorize();

        public void OnRequestPersonalProfileDataPermissionButtonClick() => 
            PlayerAccount.RequestPersonalProfileDataPermission();

        public void OnGetProfileDataButtonClick()
        {
            PlayerAccount.GetProfileData((result) =>
            {
                string name = result.publicName;
                if (string.IsNullOrEmpty(name))
                    name = English;
                Debug.Log($"My id = {result.uniqueID}, name = {name}");
            });
        }

        public void OnSetLeaderboardScoreButtonClick() => 
            Leaderboard.SetScore("TheBestLevel", Random.Range(1, 100));

        public void OnGetLeaderboardEntriesButtonClick()
        {
            Leaderboard.GetEntries("TheBestLevel", (result) =>
            {
                Debug.Log($"My rank = {result.userRank}");
                foreach (var entry in result.entries)
                {
                    string name = entry.player.publicName;
                    
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

                    Debug.Log(name + " " + entry.score);
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
    }
}