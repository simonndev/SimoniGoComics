using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ContosoInc.Presentation.Services
{
    public interface ILocalizerService
    {
        /// <summary>
        /// Set localization
        /// </summary>
        /// <param name="locale"></param>
        void SetLocale(string locale);

        /// <summary>
        /// Set localization
        /// </summary>
        /// <param name="culture"></param>
        void SetLocale(CultureInfo culture);

        /// <summary>
        /// Get a localized string by key
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns></returns>
        string GetLocalizedString(string key);

        /// <summary>
        /// Supported languages
        /// </summary>
        IList<CultureInfo> SupportedLanguages { get; }

        /// <summary>
        /// The current selected language
        /// </summary>
        CultureInfo SelectedLanguage { get; set; }
    }

    public class LocalizerService : ILocalizerService
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="culture"></param>
        public LocalizerService(string culture)
        {
            this.SupportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures).Where(c => c.IetfLanguageTag.Equals("de-DE") ||
                                                                                                   c.IetfLanguageTag.Equals("en-US"))
                                                                                       .ToList<CultureInfo>();
            this.SetLocale(culture);
        }

        /// <summary>
        /// Set localization
        /// </summary>
        /// <param name="locale"></param>
        public void SetLocale(string locale)
        {
            //LocalizeDictionary.Instance.Culture = CultureInfo.GetCultureInfo(locale);
        }

        /// <summary>
        /// Set localization
        /// </summary>
        /// <param name="culture"></param>
        public void SetLocale(CultureInfo culture)
        {
            //LocalizeDictionary.Instance.Culture = culture;
        }

        /// <summary>
        /// Get localized string from resource dictionary
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetLocalizedString(string key)
        {
            string uiString = string.Empty;
            //LocExtension locExtension = new LocExtension(key);
            //locExtension.ResolveLocalizedValue(out uiString);
            return uiString;
        }

        /// <summary>
        /// List with supported languages
        /// </summary>
        public IList<CultureInfo> SupportedLanguages { get; private set; }

        /// <summary>
        /// The current selected language
        /// </summary>
        public CultureInfo SelectedLanguage
        {
            get { return null; } //LocalizeDictionary.Instance.Culture; }
            set { this.SetLocale(value); }
        }
    }
}
