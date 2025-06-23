using UnityEngine;
using Zenject;

namespace Localization
{
    public static class LocalizationExtensions
    {
        private static ILocalizationSource _localizationSource;

        private static ILocalizationSource LocalizationSource => _localizationSource ??= ProjectContext.Instance.Container.TryResolve<ILocalizationSource>();

        public static string Localize(this string key, params object[] args)
        {
            return LocalizationSource.Translate(key, args);
        }
    }
}
