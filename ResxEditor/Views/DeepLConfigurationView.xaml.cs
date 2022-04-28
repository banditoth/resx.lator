using ResxEditor.ViewModels;

namespace ResxEditor.Views;

public partial class DeepLConfigurationView : ContentPage
{
    public DeepLConfigurationView()
    {
        InitializeComponent();
        DeepLConfigurationViewModel vm = ServiceProviderEverywhere.Instance.GetService<DeepLConfigurationViewModel>();
        BindingContext = vm;
        vm.Initalize();
    }
}
