namespace DeliveryOfGoods.Model
{
    public static class Config
    {
        public static float ConveyorSpeed { get; private set; } = 1f;
        public static float SpawnSpeed { get; private set; } = 5f;
        public static int MaxNumberBoxs { get; private set; } = 5;

        private static float _stepSpeedUpgrade = 0.2f;

        public static void UpgradeConveyor()
        {
            ConveyorSpeed += _stepSpeedUpgrade;
        }

        public static void UpgradeSpawn()
        {
            SpawnSpeed -= _stepSpeedUpgrade;
        }

        public static void UpgradeNumberBoxs()
        {
            MaxNumberBoxs++;
        }
    }
}