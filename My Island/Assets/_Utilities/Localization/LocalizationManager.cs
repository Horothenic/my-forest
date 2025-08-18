using System.Collections.Generic;
using System.Globalization;

namespace Localization
{
    public partial class LocalizationManager
    {
        #region FIELDS

        private const string DEFAULT_LANGUAGE_CODE = "en";
        
        private readonly Dictionary<string, BetterCultureInfo> _availableLanguages = new Dictionary<string, BetterCultureInfo>
        {
            { "en", new BetterCultureInfo("en", "English", CultureInfo.InvariantCulture) },
            { "es-MX", new BetterCultureInfo("es-MX", "Español", CultureInfo.InvariantCulture) },
            { "pt-BR", new BetterCultureInfo("pt-BR", "Português Brasileiro", CultureInfo.InvariantCulture) },
        };
        
        #endregion
        
        #region CONSTRUCTORS

        public LocalizationManager()
        {
            LoadTranslationFiles();
        }
        
        #endregion
        
        #region METHODS

        private void LoadTranslationFiles()
        {
            // TODO: Create a code that loads only the desired language.
        }
        
        #endregion
    }
    
    public partial class LocalizationManager : ILocalizationSource
    {
        IReadOnlyDictionary<string, BetterCultureInfo> ILocalizationSource.AvailableLanguages => _availableLanguages;

        void ILocalizationSource.SetLanguage(string languageCode)
        {
            // TODO: Create the logic to change languages.
        }

        string ILocalizationSource.Translate(string key, params object[] args)
        {
            // TODO: Find translation first.
            return string.Format(key, args);
        }
    }
}
