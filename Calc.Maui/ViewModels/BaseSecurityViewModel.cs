using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Calc.Infrastructure.Services;

namespace Calc.Maui.ViewModels;

public partial class BaseSecurityViewModel : ObservableObject
{
    protected readonly SecurityService SecurityService;

    public BaseSecurityViewModel(SecurityService securityService)
    {
        SecurityService = securityService;
        SecurityService.ResetTimer();
    }

    [RelayCommand]
    public void ResetPanic() => SecurityService.ResetTimer();
}