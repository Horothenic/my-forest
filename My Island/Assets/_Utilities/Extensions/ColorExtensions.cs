namespace UnityEngine
{
    public static class ColorExtensions
    {
        private const float MAX_COLOR_VALUE = 255f;
        
        public static Color HexToColor(string hex)
        {
            if (hex.Length != 6)
            {
                Debug.LogError("Invalid hex color code");
                return Color.white;
            }

            var r = System.Convert.ToInt32(hex.Substring(0, 2), 16) / MAX_COLOR_VALUE;
            var g = System.Convert.ToInt32(hex.Substring(2, 2), 16) / MAX_COLOR_VALUE;
            var b = System.Convert.ToInt32(hex.Substring(4, 2), 16) / MAX_COLOR_VALUE;

            return new Color(r, g, b);
        }
    }
}
