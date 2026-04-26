namespace Calc.Core.Interfaces;

public interface INavigationService
{
    Task GoToAsync(string route, IDictionary<string, object>? parameters = null);
    Task GoBackAsync();
}