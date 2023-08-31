using UnityEngine;

namespace DeliveryOfGoods.Model
{
    public static class Config
    {
        public static readonly string NameScene = "Level";

        private static readonly string _spawnSpeed = "SpawnSpeed";
        private static readonly string _maxNumberBoxs = "MaxNumberBox";
        private static readonly string _currentDeliverBoxs = "CurrentDeliverBoxs";
        private static readonly string _currentLevel = "CurrentLevel";

        private static float _stepSpeedImprove = 0.2f;
        private static int _stepLevel = 10;
        private static int _tragetLevel = _stepLevel;

        public static float SpawnSpeed { get; private set; } = 3f;
        public static int MaxNumberBoxs { get; private set; } = 5;
        public static int CurrentDeliverBoxs { get; private set; } = 3;
        public static int CurrentLevel { get; private set; } = 0;

        public static void Init()
        {
            if (PlayerPrefs.HasKey(_spawnSpeed))
                SpawnSpeed = PlayerPrefs.GetFloat(_spawnSpeed);
            if (PlayerPrefs.HasKey(_maxNumberBoxs))
                MaxNumberBoxs = PlayerPrefs.GetInt(_maxNumberBoxs);
            if (PlayerPrefs.HasKey(_currentDeliverBoxs))
                CurrentDeliverBoxs = PlayerPrefs.GetInt(_currentDeliverBoxs);
            if (PlayerPrefs.HasKey(_currentLevel))
                CurrentLevel = PlayerPrefs.GetInt(_currentLevel);
        }

        public static void Improve()
        {
            if (CurrentLevel >= _tragetLevel)
            {
                CurrentDeliverBoxs++;
                _tragetLevel += _stepLevel;
            }

            SpawnSpeed += _stepSpeedImprove;
            MaxNumberBoxs++;
            CurrentLevel++;

            PlayerPrefs.SetFloat(_spawnSpeed, SpawnSpeed);
            PlayerPrefs.SetInt(_maxNumberBoxs, MaxNumberBoxs);
            PlayerPrefs.SetInt(_currentDeliverBoxs, CurrentDeliverBoxs);
            PlayerPrefs.SetInt(_currentLevel, CurrentLevel);
        }
    }
}