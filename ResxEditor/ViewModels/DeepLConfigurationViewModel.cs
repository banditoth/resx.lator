using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DeepL;
using ResxEditor.Interfaces;
using ResxEditor.Services;
using ResxEditor.Views;

namespace ResxEditor.ViewModels
{
	public partial class DeepLConfigurationViewModel : BaseViewModel
	{
        [ObservableProperty]
		private string _authKey;

        [ObservableProperty]
		private TextTranslateOptions _translationOptions;

        private readonly IErrorHandler errorHandler;
        //private readonly IServiceCollection serviceCollection;
        private readonly IServiceProvider serviceProvider;
        private readonly IDialogHandler dialogHandler;

        public DeepLConfigurationViewModel(IErrorHandler errorHandler, IServiceProvider serviceProvider,
            IDialogHandler dialogHandler)
		{
            this.errorHandler = errorHandler;
            this.serviceProvider = serviceProvider;
            this.dialogHandler = dialogHandler;
        }

        public void Initalize()
        {
            try
            {
                AuthKey = Preferences.Get(DeeplTranslatorService.AuthKeySettingKey, null);
            }
            catch (Exception ex)
            {
                errorHandler.HandleException(ex, "Could not get saved auth key", "Authentication key getting failure");
            }
        }

        [ICommand]
        private async Task OpenDeeplPortal()
        {
            try
            {
                await Browser.OpenAsync("https://www.deepl.com/pro-account/summary");
            }
            catch (Exception ex)
            {
                errorHandler.HandleException(ex, "Could not open deepl portal, exception occured", "Could not open portal.");
            }
        }

        [ICommand]
		private async Task SaveSettings()
        {
            try
            {
                if(string.IsNullOrWhiteSpace(_authKey))
                {
                    _ = dialogHandler.DisplayAlert("Error", "Providing authentication key is mandatory", "Ok");
                    return;
                }

                DeeplTranslatorService deeplTranslatorService = (DeeplTranslatorService)serviceProvider.GetService<ITranslator>();

                deeplTranslatorService.SaveSettings(new Dictionary<string, object>()
                {
                    { DeeplTranslatorService.AuthKeySettingKey , _authKey },
                    { DeeplTranslatorService.TranslateOptionsSettingKey , _translationOptions },
                });

                await Shell.Current.GoToAsync("///MainPage");
            }
            catch (Exception ex)
            {
                errorHandler.HandleException(ex, "Could not save deepl configuration settings, exception occured", "Could not set DeepL as trasnlator");
            }
        }
	}
}

