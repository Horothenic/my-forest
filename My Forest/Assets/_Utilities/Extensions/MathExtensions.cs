namespace UnityEngine
{
    public static class MathExtensions
    {
        private const float HALF_DIVIDER = 2f;

        public static float Half(this uint value)
        {
            return value / HALF_DIVIDER;
        }

        public static float Half(this int value)
        {
            return value / HALF_DIVIDER;
        }

        public static double Half(this long value)
        {
            return value / HALF_DIVIDER;
        }

        public static float Half(this float value)
        {
            return value / HALF_DIVIDER;
        }

        public static double Half(this double value)
        {
            return value / HALF_DIVIDER;
        }

        public static float Squared(this float value)
        {
            return value * value;
        }

        public static double Squared(this double value)
        {
            return value * value;
        }
    }
}
