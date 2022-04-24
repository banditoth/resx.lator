using System.Resources.NetStandard;
using ResxEditor.ViewModels;
using ResxEditor.Views;

namespace ResxEditor;


public partial class App : Application
{
	public App(IServiceProvider serviceProvider)
	{
		InitializeComponent();

		ServiceProviderEverywhere.SetProvider(serviceProvider);
		MainPage = new AppShell();
	}
}
