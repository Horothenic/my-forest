namespace Lean.Localization
{
    public static class LocalizationExtensions
    {
        public static string Localize(string key)
        {
            return LeanLocalization.GetTranslationText(key);
        }

        public static string Localize(string key, params object[] formatValues)
        {
            return string.Format(LeanLocalization.GetTranslationText(key), formatValues);
        }
    }
}
