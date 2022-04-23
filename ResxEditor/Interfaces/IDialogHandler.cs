using System;
namespace ResxEditor.Interfaces
{
	public interface IDialogHandler
	{
		Task DisplayAlert(string title, string message, string confirm);

		Task<string> DisplayPrompt(string title, string message, string confirm);
	}
}

