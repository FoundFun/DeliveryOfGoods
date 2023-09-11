using System;
using UnityEngine;

namespace DeliveryOfGoods.Model
{
    [CreateAssetMenu(fileName = "Config", order = 51)]
    public class Config : ScriptableObject
    {
        public readonly string NameScene = "Level";

        private const string SpawnSpeedText = "SpawnSpeed";
        private const string CurrentDeliverBoxText = "CurrentDeliverBox";
        private const string CurrentLevelText = "CurrentLevel";

        private const float MinSpawnSpeed = 0.7f;
        private const float StepSpeedImprove = -0.1f;
        private const int StepLevel = 10;
        
        private int _tragetLevel = StepLevel;

        public float SpawnSpeed { get; private set; } = 2f;
        public int CurrentDeliverBox { get; private set; } = 3;
        public int CurrentLevel { get; private set; }

        public event Action<int> ChangedTargetScore;

        public void UpdateValue()
        {
            if (PlayerPrefs.HasKey(SpawnSpeedText))
                SpawnSpeed = PlayerPrefs.GetFloat(SpawnSpeedText);
            if (PlayerPrefs.HasKey(CurrentDeliverBoxText))
                CurrentDeliverBox = PlayerPrefs.GetInt(CurrentDeliverBoxText);
            if (PlayerPrefs.HasKey(CurrentLevelText))
                CurrentLevel = PlayerPrefs.GetInt(CurrentLevelText);
            
            ChangedTargetScore?.Invoke(CurrentDeliverBox);
        }
        
        public void Improve()
        {
            if (CurrentLevel >= _tragetLevel)
            {
                CurrentDeliverBox++;
                _tragetLevel += StepLevel;
                ChangedTargetScore?.Invoke(CurrentDeliverBox);
            }

            if (SpawnSpeed > MinSpawnSpeed)
                SpawnSpeed += StepSpeedImprove;
            
            CurrentLevel++;

            PlayerPrefs.SetFloat(SpawnSpeedText, SpawnSpeed);
            PlayerPrefs.SetInt(CurrentDeliverBoxText, CurrentDeliverBox);
            PlayerPrefs.SetInt(CurrentLevelText, CurrentLevel);
        }
    }
}