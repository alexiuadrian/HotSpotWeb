using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace HotSpotWeb.Localization
{
    public static class HotSpotWebLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(HotSpotWebConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(HotSpotWebLocalizationConfigurer).GetAssembly(),
                        "HotSpotWeb.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
