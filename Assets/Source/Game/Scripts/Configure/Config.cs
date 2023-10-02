using System;
using UnityEngine;

namespace Source.Game.Scripts.Configure
{
    [CreateAssetMenu(fileName = "Config", order = 51)]
    public class Config : ScriptableObject
    {
        public const string NameScene = "Level";

        private const string SpawnSpeedText = "SpawnSpeed";
        private const string CurrentDeliverBoxText = "CurrentDeliverBox";
        private const string CurrentLevelText = "CurrentLevel";
        private const string ScoreLeaderBordText = "ScoreLeaderBord";

        private const float MinSpawnSpeed = 1.5f;
        private const float StepSpeedImprove = -0.02f;
        private const int StepLevel = 10;
        
        private int _targetLevel = StepLevel;

        public float SpawnSpeed { get; private set; } = 2.5f;
        public int CurrentDeliverBox { get; private set; } = 3;
        public int CurrentLevel { get; private set; } = 0;

        public bool IsGaming { get; private set; }
        
        public int ScoreLeaderBord { get; private set; }
        
        public event Action<int> ChangedTargetScore;

        public void EnableGame() => 
            IsGaming = true;

        public void DisableGame() => 
            IsGaming = false;

        public void UpdateValue()
        {
            if (PlayerPrefs.HasKey(SpawnSpeedText))
                SpawnSpeed = PlayerPrefs.GetFloat(SpawnSpeedText);
            if (PlayerPrefs.HasKey(CurrentDeliverBoxText))
                CurrentDeliverBox = PlayerPrefs.GetInt(CurrentDeliverBoxText);
            if (PlayerPrefs.HasKey(CurrentLevelText))
                CurrentLevel = PlayerPrefs.GetInt(CurrentLevelText);
            if (PlayerPrefs.HasKey(ScoreLeaderBordText))
                ScoreLeaderBord = PlayerPrefs.GetInt(ScoreLeaderBordText);

            ChangedTargetScore?.Invoke(CurrentDeliverBox);
        }

        public void Improve()
        {
            if (CurrentLevel >= _targetLevel)
            {
                CurrentDeliverBox++;
                _targetLevel += StepLevel;
                ChangedTargetScore?.Invoke(CurrentDeliverBox);
            }

            if (SpawnSpeed > MinSpawnSpeed)
                SpawnSpeed += StepSpeedImprove;
            
            CurrentLevel++;

            PlayerPrefs.SetFloat(SpawnSpeedText, SpawnSpeed);
            PlayerPrefs.SetInt(CurrentDeliverBoxText, CurrentDeliverBox);
            PlayerPrefs.SetInt(CurrentLevelText, CurrentLevel);
        }

        public void AddScoreLeaderBord()
        {
            ScoreLeaderBord++;
            PlayerPrefs.SetInt(ScoreLeaderBordText, ScoreLeaderBord);
        }
    }
}