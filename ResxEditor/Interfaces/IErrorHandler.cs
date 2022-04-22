using System;
namespace ResxEditor.Interfaces
{
	public interface IErrorHandler
	{
		void HandleException(Exception ex, string details, string uiMessage = null);
	}
}

