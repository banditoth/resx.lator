using System;
using ResxEditor.Enumerations;

namespace ResxEditor.Interfaces
{
	public interface ITranslator
	{
		Task TranslateAsync(Language fromLanguage, Language toLanguage, string s);
	}
}

