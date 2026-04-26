using Calc.Core.Interfaces;


namespace Calc.Infrastructure.Services;

public class SecurityService
{
    private readonly INavigationService _navigationService;
    private IDispatcherTimer _panicTimer;
    private const int TimeoutSeconds = 60;

    // Denna variabel håller den aktiva krypteringsnyckeln i RAM. 
    // Den sparas aldrig på disk.
    public string? CurrentSessionKey { get; private set; }

    public SecurityService(INavigationService navigationService)
    {
        _navigationService = navigationService;
        
        _panicTimer = Application.Current.Dispatcher.CreateTimer();
        _panicTimer.Interval = TimeSpan.FromSeconds(TimeoutSeconds);
        _panicTimer.Tick += async (s, e) => await TriggerPanic();
    }

    public bool Authenticate(string inputCode)
    {
        string storedHash = Preferences.Get("UserSecretHash", string.Empty);
        string inputHash = EncryptionService.ComputeHash(inputCode);

        if (string.IsNullOrEmpty(storedHash))
        {
            // Första gången: Spara hashen
            Preferences.Set("UserSecretHash", inputHash);
            CurrentSessionKey = inputCode;
            ResetTimer();
            return true;
        }

        if (inputHash == storedHash)
        {
            CurrentSessionKey = inputCode;
            ResetTimer();
            return true;
        }

        return false;
    }

    public bool IsFirstRun => !Preferences.ContainsKey("UserSecretHash");

    public void ResetTimer()
    {
        _panicTimer.Stop();
        _panicTimer.Start();
    }

    public async Task TriggerPanic()
    {
        CurrentSessionKey = null; // Rensa nyckeln från RAM omedelbart
        _panicTimer.Stop();
        await Shell.Current.GoToAsync("//MainPage");
    }
}
