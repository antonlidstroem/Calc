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

    protected override void OnSleep()
    {
        // Appen går till bakgrunden -> Aktivera panic direkt
        _securityService.TriggerPanic().Wait();
    }

    protected override void OnResume()
    {
        // Säkerställ att vi är på CalculatorView när vi kommer tillbaka
        _securityService.TriggerPanic().Wait();
    }
}
