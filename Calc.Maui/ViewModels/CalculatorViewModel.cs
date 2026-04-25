using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Calc.Core.Interfaces;
using System.Globalization;

namespace Calc.App.ViewModels;

public partial class CalculatorViewModel : ObservableObject
{
    private readonly ICalculatorService _calculatorService;
    private bool _isNewEntry = true;
    private const double Tolerance = 0.0001;

    [ObservableProperty]
    private string _displayText = "0";

    [ObservableProperty]
    private double _lastResult;

    public CalculatorViewModel(ICalculatorService calculatorService)
    {
        _calculatorService = calculatorService;
    }

    #region Commands

    [RelayCommand]
    private void AddDigit(string digit)
    {
        if (_isNewEntry || DisplayText == "0")
        {
            DisplayText = digit;
            _isNewEntry = false;
        }
        else
        {
            DisplayText += digit;
        }
    }

    [RelayCommand]
    private void AddOperator(string op)
    {
        // Förhindra dubbla operatorer
        if (!string.IsNullOrEmpty(DisplayText) && !EndsWithOperator())
        {
            DisplayText += op;
            _isNewEntry = false;
        }
    }

    [RelayCommand]
    private void AddDecimal()
    {
        // Enkel validering för att inte lägga till flera punkter i samma tal
        // I en produktion-app bör man splitta på operatorer och kolla sista segmentet
        if (!DisplayText.EndsWith("."))
        {
            DisplayText += ".";
            _isNewEntry = false;
        }
    }

    [RelayCommand]
    private void Backspace()
    {
        if (DisplayText.Length > 1)
            DisplayText = DisplayText.Substring(0, DisplayText.Length - 1);
        else
            DisplayText = "0";
    }

    [RelayCommand]
    private void Clear()
    {
        DisplayText = "0";
        LastResult = 0;
        _isNewEntry = true;
    }

    [RelayCommand]
    private void Calculate()
    {
        try
        {
            LastResult = _calculatorService.EvaluateExpression(DisplayText);
            DisplayText = LastResult.ToString(CultureInfo.InvariantCulture);
            _isNewEntry = true;
        }
        catch
        {
            DisplayText = "Error";
            _isNewEntry = true;
        }
    }

   [RelayCommand]
private async Task OnPlusLongPress()
{
    if (Math.Abs(LastResult - 777) < Tolerance)
    {
        await _navigationService.GoToAsync(nameof(Views.ArticlesView));
    }
    else if (Math.Abs(LastResult - 316) < Tolerance)
    {
        await _navigationService.GoToAsync(nameof(Views.EditorView));
    }
}
