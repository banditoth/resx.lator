using ResxEditor.Views;

namespace ResxEditor;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(ResourceEditorView), typeof(ResourceEditorView));
        Routing.RegisterRoute(nameof(DeepLConfigurationView), typeof(DeepLConfigurationView));
        Routing.RegisterRoute(nameof(TranslatorChooserView), typeof(TranslatorChooserView));
    }
}
