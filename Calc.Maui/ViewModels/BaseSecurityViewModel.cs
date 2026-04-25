public partial class BaseSecurityViewModel : ObservableObject
{
    protected readonly SecurityService SecurityService;

    public BaseSecurityViewModel(SecurityService securityService)
    {
        SecurityService = securityService;
        SecurityService.ResetTimer(); // Starta timern när vyn laddas
    }

    [RelayCommand]
    public void ResetPanic() => SecurityService.ResetTimer();
}
