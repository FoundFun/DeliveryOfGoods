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

        public static void Improve()
        {
            SpawnSpeed += _stepSpeedImprove;
            MaxNumberBoxs++;
            CurrentDeliverBoxs++;
            CurrentLevel++;
        }
    }
}