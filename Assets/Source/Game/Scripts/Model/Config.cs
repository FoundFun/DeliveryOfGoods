using UnityEngine;

namespace DeliveryOfGoods.Model
{
    [CreateAssetMenu(fileName = "Config", order = 51)]
    public class Config : ScriptableObject
    {
        public readonly string NameScene = "Level";

        private const string SpawnSpeedText = "SpawnSpeed";
        private const string MaxNumberBoxText = "MaxNumberBox";
        private const string CurrentDeliverBoxText = "CurrentDeliverBox";
        private const string CurrentLevelText = "CurrentLevel";

        private const float StepSpeedImprove = 0.2f;
        private const int StepLevel = 10;
        
        private int _tragetLevel = StepLevel;

        public float SpawnSpeed { get; private set; } = 3f;
        public int MaxNumberBox { get; private set; } = 5;
        public int CurrentDeliverBox { get; private set; } = 3;
        public int CurrentLevel { get; private set; } = 0;

        public void Init()
        {
            if (PlayerPrefs.HasKey(SpawnSpeedText))
                SpawnSpeed = PlayerPrefs.GetFloat(SpawnSpeedText);
            if (PlayerPrefs.HasKey(MaxNumberBoxText))
                MaxNumberBox = PlayerPrefs.GetInt(MaxNumberBoxText);
            if (PlayerPrefs.HasKey(CurrentDeliverBoxText))
                CurrentDeliverBox = PlayerPrefs.GetInt(CurrentDeliverBoxText);
            if (PlayerPrefs.HasKey(CurrentLevelText))
                CurrentLevel = PlayerPrefs.GetInt(CurrentLevelText);
        }

        public void Improve()
        {
            if (CurrentLevel >= _tragetLevel)
            {
                CurrentDeliverBox++;
                _tragetLevel += StepLevel;
            }

            SpawnSpeed += StepSpeedImprove;
            MaxNumberBox++;
            CurrentLevel++;

            PlayerPrefs.SetFloat(SpawnSpeedText, SpawnSpeed);
            PlayerPrefs.SetInt(MaxNumberBoxText, MaxNumberBox);
            PlayerPrefs.SetInt(CurrentDeliverBoxText, CurrentDeliverBox);
            PlayerPrefs.SetInt(CurrentLevelText, CurrentLevel);
        }
    }
}