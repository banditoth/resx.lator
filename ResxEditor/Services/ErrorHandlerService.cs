using System;
using Microsoft.Extensions.Logging;
using ResxEditor.Interfaces;

namespace ResxEditor.Services
{
    public class ErrorHandlerService : IErrorHandler
    {
        private readonly IDialogHandler dialogHandler;

        public ErrorHandlerService(IDialogHandler dialogHandler)
        {
            this.dialogHandler = dialogHandler;
        }

        public void HandleException(Exception ex, string details, string uiMessage = null)
        {
            if(string.IsNullOrWhiteSpace(uiMessage) == false)
            {
                _ = dialogHandler.DisplayAlert("An error occured", uiMessage, "Confirm");
            }
        }
    }
}

