using System.Resources.NetStandard;
using ResxEditor.ViewModels;
using ResxEditor.Views;

namespace ResxEditor;


public partial class App : Application
{
	public App(IServiceProvider serviceProvider)
	{
		InitializeComponent();

		MainPage = new DeepLConfigurationView(serviceProvider.GetService<DeepLConfigurationViewModel>());
	}
}
