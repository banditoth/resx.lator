using ResxEditor.ViewModels;

namespace ResxEditor.Views;

public partial class ResourceEditorView : ContentPage
{
    public ResourceEditorView()
    {
        InitializeComponent();
        BindingContext = ServiceProviderEverywhere.Instance.GetService<ResourceEditorViewModel>();
    }
}
