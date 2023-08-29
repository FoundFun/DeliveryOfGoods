namespace DeliveryOfGoods.Model
{
    public static class Config
    {
        public static readonly string NameScene = "Level";

        public static float SpawnSpeed { get; private set; } = 3f;
        public static int MaxNumberBoxs { get; private set; } = 5;
        public static int CurrentDeliverBoxs { get; private set; } = 3;
        public static int CurrentLevel { get; private set; } = 0;

        private static float _stepSpeedImprove = 0.2f;
        private static int _stepLevel = 10;
        private static int _tragetLevel = CurrentLevel + _stepLevel;

        public static void Improve()
        {
            if (CurrentLevel == _tragetLevel)
            {
                CurrentDeliverBoxs++;
                _tragetLevel += _stepLevel;
            }

            SpawnSpeed += _stepSpeedImprove;
            MaxNumberBoxs++;
            CurrentLevel++;
        }

        public static void GetValueCloud(string data)
        {
            CurrentLevel = int.Parse(data);
        }
    }
}