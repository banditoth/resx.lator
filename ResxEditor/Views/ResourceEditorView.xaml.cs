using ResxEditor.ViewModels;

namespace ResxEditor.Views;

public partial class ResourceEditorView : ContentPage
{
    public ResourceEditorView()
    {
        InitializeComponent();
        ResourceEditorViewModel vm = ServiceProviderEverywhere.Instance.GetService<ResourceEditorViewModel>();
        BindingContext = vm;
        vm.Initalize();
    }
}
