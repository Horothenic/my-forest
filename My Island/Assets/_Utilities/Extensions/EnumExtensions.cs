using System;

namespace UnityEngine
{
    public static class EnumExtensions
    {
        public static T Random<T>() where T : System.Enum
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("[ENUM EXTENSIONS] T must be an enumerated type");
            }

            var values = (T[])Enum.GetValues(typeof(T));
            var randomIndex = UnityEngine.Random.Range(0, values.Length);
            return values[randomIndex];
        }
        
        public static int Index<T>(this T enumValue) where T : Enum
        {
            return Convert.ToInt32(enumValue);
        }
    }
}
