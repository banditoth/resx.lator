using System;
using ResxEditor.Enumerations;

namespace ResxEditor.Interfaces
{
	public interface ITranslator
	{
		Task<string> TranslateAsync(Language fromLanguage, Language toLanguage, string s);

		void SaveSettings(Dictionary<string, object> settingsToSave);
	}
}

