using System.Resources.NetStandard;

namespace ResxEditor;


public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
