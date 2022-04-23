using Microsoft.Maui.Hosting;
using ResxEditor.Interfaces;
using ResxEditor.Services;
using ResxEditor.ViewModels;

namespace ResxEditor;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddTransient(typeof(IDialogHandler), typeof(DialogHandlerService));
		builder.Services.AddTransient(typeof(IErrorHandler), typeof(ErrorHandlerService));
		builder.Services.AddTransient(typeof(IResourceAssetManager), typeof(ResourceAssetManagerService));
		builder.Services.AddTransient(typeof(ITranslator), typeof(DeeplTranslatorService));
		builder.Services.AddTransient(typeof(ResourceEditorViewModel));
		builder.Services.AddTransient(typeof(DeepLConfigurationViewModel));

		return builder.Build();
	}
}
