using ResxEditor.ViewModels;

namespace ResxEditor.Views;

public partial class ResourceEditorView : ContentPage
{
    public ResourceEditorView(ResourceEditorViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
