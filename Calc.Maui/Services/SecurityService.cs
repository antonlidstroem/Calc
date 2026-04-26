using Calc.App.Services;

namespace Calc.Infrastructure.Services;

public class SecurityService
{
    private readonly INavigationService _navigationService;
    private IDispatcherTimer _panicTimer;
    private const int TimeoutSeconds = 60;

    public SecurityService(INavigationService navigationService)
    {
        _navigationService = navigationService;
        
        _panicTimer = Application.Current.Dispatcher.CreateTimer();
        _panicTimer.Interval = TimeSpan.FromSeconds(TimeoutSeconds);
        _panicTimer.Tick += async (s, e) => await TriggerPanic();
    }

    public void ResetTimer()
    {
        _panicTimer.Stop();
        _panicTimer.Start();
    }

    public void StopTimer() => _panicTimer.Stop();

    public async Task TriggerPanic()
    {
        _panicTimer.Stop();
        // Updated route to //MainPage to match AppShell.xaml
        await Shell.Current.GoToAsync("//MainPage");
    }
}
