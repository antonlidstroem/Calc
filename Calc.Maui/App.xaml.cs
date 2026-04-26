using Calc.Maui.Services;
using Calc.Maui.Views;

namespace Calc.Maui;

public partial class App : Application
{
    private readonly SecurityService _securityService;

    public App(SecurityService securityService)
    {
        InitializeComponent();

        _securityService = securityService;

        RegisterRoutes();

        MainPage = new AppShell();
    }

    private void RegisterRoutes()
    {
        Routing.RegisterRoute(nameof(ArticlesView), typeof(ArticlesView));
        Routing.RegisterRoute(nameof(EditorView), typeof(EditorView));
        Routing.RegisterRoute(nameof(ArticleDetailView), typeof(ArticleDetailView));
    }

    protected override async void OnSleep()
    {
        await _securityService.TriggerPanic();
    }

    protected override async void OnResume()
    {
        await _securityService.TriggerPanic();
    }
}