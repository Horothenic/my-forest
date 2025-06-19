using System.Collections.Generic;

namespace Localization
{
    public interface ILocalizationSource
    {
        IReadOnlyDictionary<string, BetterCultureInfo> AvailableLanguages { get; }
        
        void SetLanguage(string languageCode);
        string Translate(string key, params object[] args);
    }
}
