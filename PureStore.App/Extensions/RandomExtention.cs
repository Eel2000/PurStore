namespace PureStore.App.Extensions
{
    public static class RandomExtention
    {
        private const double BASE_PERCENTAGE = 100.0;

        public static bool NextBool(this Random random, int percentage = 50)
        {
            return random.NextDouble() < percentage / BASE_PERCENTAGE;
        }
    }
}
