using Reflex.Core;

namespace Localization
{
    public static class LocalizationExtensions
    {
        private static ILocalizationSource _localizationSource;
        private static ILocalizationSource LocalizationSource => _localizationSource ??=  Container.ProjectContainer.Resolve<ILocalizationSource>();

        public static string Localize(this string key, params object[] args)
        {
            return LocalizationSource.Translate(key, args);
        }
    }
}
