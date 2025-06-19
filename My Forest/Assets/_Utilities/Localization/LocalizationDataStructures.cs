using System.Globalization;

namespace Localization
{
    public struct BetterCultureInfo
    {
        public string LanguageCode { get; private set; }
        public string LanguageName { get; private set; }
        public CultureInfo CultureInfo { get; private set; }

        public BetterCultureInfo(string languageCode, string languageName, CultureInfo cultureInfo)
        {
            LanguageCode = languageCode;
            LanguageName = languageName;
            CultureInfo = cultureInfo;
        }
    }
}
