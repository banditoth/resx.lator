using System;
using DeepL;
using DeepL.Model;
using ResxEditor.Enumerations;
using ResxEditor.Interfaces;

namespace ResxEditor.Services
{
    public class DeeplTranslatorService : ITranslator
    {
        public const string AuthKeySettingKey = "auth_key";
        public const string TranslateOptionsSettingKey = "translate_options";
        private readonly string _authKey;
        private readonly TextTranslateOptions _options;
        private readonly IPreferences _preferences;

        public DeeplTranslatorService(IPreferences preferences)
        {
            _preferences = preferences;
            _authKey = _preferences.Get<string>(AuthKeySettingKey, null);
            _options = _preferences.Get<TextTranslateOptions>(TranslateOptionsSettingKey, null);
        }

        public void SaveSettings(Dictionary<string, object> settingsToSave)
        {
            if (settingsToSave == null)
                return;

            foreach (var setting in settingsToSave)
            {
                switch (setting.Key)
                {
                    case AuthKeySettingKey:
                        _preferences.Set(AuthKeySettingKey, setting.Value);
                        break;
                    case TranslateOptionsSettingKey:
                        _preferences.Set(TranslateOptionsSettingKey, setting.Value);
                        break;
                    default:
                        throw new Exception("Unknown setting key: " + setting.Key);
                }
            }
        }

        public async Task<string> TranslateAsync(Enumerations.Language fromLanguage, Enumerations.Language toLanguage, string s)
        {
            Translator translator = new Translator(_authKey);
            TextResult results = await translator.TranslateTextAsync(s, GetLanguageCodeFromLanguageEnum(fromLanguage), GetLanguageCodeFromLanguageEnum(toLanguage), _options);
            return results?.Text;
        }

        private string GetLanguageCodeFromLanguageEnum(Enumerations.Language language)
        {
            switch (language)
            {
                case Enumerations.Language.Bulgarian : return LanguageCode.Bulgarian;
				case Enumerations.Language.Chinese : return LanguageCode.Chinese;
				case Enumerations.Language.Czech : return LanguageCode.Czech;
				case Enumerations.Language.Danish : return LanguageCode.Danish;
				case Enumerations.Language.Dutch : return LanguageCode.Dutch;
				case Enumerations.Language.EnglishUS : return LanguageCode.EnglishAmerican;
				case Enumerations.Language.EnglishGB : return LanguageCode.EnglishBritish;
				case Enumerations.Language.Estonian : return LanguageCode.Estonian;
				case Enumerations.Language.Finnish : return LanguageCode.Finnish;
				case Enumerations.Language.French : return LanguageCode.French;
				case Enumerations.Language.German : return LanguageCode.German;
				case Enumerations.Language.Greek : return LanguageCode.Greek;
				case Enumerations.Language.Hungarian : return LanguageCode.Hungarian;
				case Enumerations.Language.Italian : return LanguageCode.Italian;
				case Enumerations.Language.Japanese : return LanguageCode.Japanese;
				case Enumerations.Language.Latvian : return LanguageCode.Latvian;
				case Enumerations.Language.Lithuanian : return LanguageCode.Lithuanian;
				case Enumerations.Language.Polish : return LanguageCode.Polish;
				case Enumerations.Language.Portuguese : return LanguageCode.Portuguese;
				case Enumerations.Language.Romanian : return LanguageCode.Romanian;
				case Enumerations.Language.Russian : return LanguageCode.Russian;
				case Enumerations.Language.Slovak : return LanguageCode.Slovak;
				case Enumerations.Language.Slovenian : return LanguageCode.Slovenian;
				case Enumerations.Language.Spanish : return LanguageCode.Spanish;
				case Enumerations.Language.Swedish : return LanguageCode.Swedish;
                default:
					throw new NotSupportedException("DeepL is not supporting this language: " + language);
            }
        }
    }
}

