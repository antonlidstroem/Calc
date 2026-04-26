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
}
