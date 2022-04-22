using System;
using Microsoft.Extensions.Logging;
using ResxEditor.Interfaces;

namespace ResxEditor.Services
{
    public class ErrorHandlerService : IErrorHandler
    {
        private readonly IDialogHandler dialogHandler;
        private readonly ILogger logger;

        public ErrorHandlerService(IDialogHandler dialogHandler, ILogger logger)
        {
            this.dialogHandler = dialogHandler;
            this.logger = logger;
        }

        public void HandleException(Exception ex, string details, string uiMessage = null)
        {
            logger.LogError(details, ex);

            if(string.IsNullOrWhiteSpace(uiMessage) == false)
            {
                _ = dialogHandler.DisplayAlert("An error occured", uiMessage, "Confirm");
            }
        }
    }
}

