using Calc.Core.Interfaces;

namespace Calc.Maui.Services;

public class NavigationService : INavigationService
{
    public Task GoToAsync(string route, IDictionary<string, object>? parameters = null)
    {
        return parameters != null 
            ? Shell.Current.GoToAsync(route, parameters) 
            : Shell.Current.GoToAsync(route);
    }

    public Task GoBackAsync() => Shell.Current.GoToAsync("..");
}
