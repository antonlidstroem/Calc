namespace Calc.App;

public partial class App : Application
{
    private readonly SecurityService _securityService;

    public App(SecurityService securityService)
    {
        InitializeComponent();
        _securityService = securityService;
        MainPage = new AppShell();
    }

    protected override async void OnSleep()
{
    await _securityService.TriggerPanic();
}


    protected override void OnResume()
    {
        // Säkerställ att vi är på CalculatorView när vi kommer tillbaka
        _securityService.TriggerPanic().Wait();
    }

Routing.RegisterRoute(nameof(ArticlesView), typeof(ArticlesView));
Routing.RegisterRoute(nameof(EditorView), typeof(EditorView));
Routing.RegisterRoute(nameof(ArticleDetailView), typeof(ArticleDetailView));

}
