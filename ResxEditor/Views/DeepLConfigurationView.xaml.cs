using ResxEditor.ViewModels;

namespace ResxEditor.Views;

public partial class DeepLConfigurationView : ContentPage
{
    public DeepLConfigurationView()
    {
        InitializeComponent();
        BindingContext = ServiceProviderEverywhere.Instance.GetService<DeepLConfigurationViewModel>();
    }
}
