namespace DeliveryOfGoods.Model
{
    public static class Config
    {
        public static float SpawnSpeed { get; private set; } = 3f;
        public static int MaxNumberBoxs { get; private set; } = 5;

        private static float _stepSpeedImprove = 0.2f;

        public static void Improve()
        {
            SpawnSpeed += _stepSpeedImprove;
            MaxNumberBoxs++;
        }
    }
}