namespace Calc.App;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(Views.ArticlesView), typeof(Views.ArticlesView));
        Routing.RegisterRoute(nameof(Views.ArticleDetailView), typeof(Views.ArticleDetailView));
        Routing.RegisterRoute(nameof(Views.EditorView), typeof(Views.EditorView));
        Routing.RegisterRoute(nameof(Views.SettingsView), typeof(Views.SettingsView));
    }
}
