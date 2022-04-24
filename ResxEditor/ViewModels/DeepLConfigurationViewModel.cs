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
            //this.serviceCollection = serviceCollection;
            this.serviceProvider = serviceProvider;
            this.dialogHandler = dialogHandler;
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

                //serviceCollection.AddTransient(typeof(ITranslator), typeof(DeeplTranslatorService));

                DeeplTranslatorService deeplTranslatorService = (DeeplTranslatorService)serviceProvider.GetService<ITranslator>();

                deeplTranslatorService.SaveSettings(new Dictionary<string, object>()
                {
                    { DeeplTranslatorService.AuthKeySettingKey , _authKey },
                    { DeeplTranslatorService.TranslateOptionsSettingKey , _translationOptions },
                });

                Application.Current.MainPage = new ResourceEditorView();
            }
            catch (Exception ex)
            {
                errorHandler.HandleException(ex, "Could not save deepl configuration settings, exception occured", "Could not set DeepL as trasnlator");
            }
        }
	}
}

