using ResxEditor.ViewModels;

namespace ResxEditor.Views;

public partial class DeepLConfigurationView : ContentPage
{
    public DeepLConfigurationView(DeepLConfigurationViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
