using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Calc.Core.Interfaces;
using Calc.Maui.Services;
using Calc.Infrastructure.Services;
using System.Globalization;

namespace Calc.Maui.ViewModels;

public partial class CalculatorViewModel : ObservableObject
{
    private readonly ICalculatorService _calculatorService;
    private readonly INavigationService _navigationService;
    private readonly SecurityService _securityService;
    private bool _isNewEntry = true;

    [ObservableProperty]
    private string _displayText = "0";

    [ObservableProperty]
    private double _lastResult;

    public CalculatorViewModel(ICalculatorService calculatorService, INavigationService navigationService, SecurityService securityService)
    {
        _calculatorService = calculatorService;
        _navigationService = navigationService;
        _securityService = securityService;
    }

    // ... Behåll AddDigit, AddOperator, Calculate osv från din originalfil ...

    [RelayCommand]
    private async Task OnPlusLongPress()
    {
        // Vi använder texten i displayen som den hemliga koden (t.ex. "1234")
        string secretCandidate = DisplayText;

        if (_securityService.IsFirstRun)
        {
            bool confirm = await Shell.Current.DisplayAlert("Setup", $"Vill du använda {secretCandidate} som din hemliga kod för att låsa krypteringen?", "Ja", "Avbryt");
            if (!confirm) return;
        }

        if (_securityService.Authenticate(secretCandidate))
        {
            // Om autentisering lyckas, skicka användaren till innehållet
            await _navigationService.GoToAsync(nameof(Views.ArticlesView));
        }
        else
        {
            // Fel kod. Vi gör ingenting för att inte avslöja att det finns en hemlig funktion.
            // Som senior arkitekt rekommenderar jag en diskret återställning.
            Clear();
        }
    }
    
    // Hjälpmetod för att rensa
    private void Clear()
    {
        DisplayText = "0";
        _isNewEntry = true;
    }
}
