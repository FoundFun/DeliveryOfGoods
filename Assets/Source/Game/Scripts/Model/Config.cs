using UnityEngine;

namespace DeliveryOfGoods.Model
{
    public static class Config
    {
        public const string NameScene = "Level";

        private const string _spawnSpeed = "SpawnSpeed";
        private static readonly string _maxNumberBox = "MaxNumberBox";
        private static readonly string _currentDeliverBox = "CurrentDeliverBox";
        private static readonly string _currentLevel = "CurrentLevel";

        private static float _stepSpeedImprove = 0.2f;
        private static int _stepLevel = 10;
        private static int _tragetLevel = _stepLevel;

        public static float SpawnSpeed { get; private set; } = 3f;
        public static int MaxNumberBox { get; private set; } = 5;
        public static int CurrentDeliverBox { get; private set; } = 3;
        public static int CurrentLevel { get; private set; } = 0;

        public static void Init()
        {
            if (PlayerPrefs.HasKey(_spawnSpeed))
                SpawnSpeed = PlayerPrefs.GetFloat(_spawnSpeed);
            if (PlayerPrefs.HasKey(_maxNumberBox))
                MaxNumberBox = PlayerPrefs.GetInt(_maxNumberBox);
            if (PlayerPrefs.HasKey(_currentDeliverBox))
                CurrentDeliverBox = PlayerPrefs.GetInt(_currentDeliverBox);
            if (PlayerPrefs.HasKey(_currentLevel))
                CurrentLevel = PlayerPrefs.GetInt(_currentLevel);
        }

        public static void Improve()
        {
            if (CurrentLevel >= _tragetLevel)
            {
                CurrentDeliverBox++;
                _tragetLevel += _stepLevel;
            }

            SpawnSpeed += _stepSpeedImprove;
            MaxNumberBox++;
            CurrentLevel++;

            PlayerPrefs.SetFloat(_spawnSpeed, SpawnSpeed);
            PlayerPrefs.SetInt(_maxNumberBox, MaxNumberBox);
            PlayerPrefs.SetInt(_currentDeliverBox, CurrentDeliverBox);
            PlayerPrefs.SetInt(_currentLevel, CurrentLevel);
        }
    }
}