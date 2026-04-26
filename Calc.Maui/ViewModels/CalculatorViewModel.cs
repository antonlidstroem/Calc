using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Calc.Core.Interfaces;
using Calc.App.Services;
using System.Globalization;

namespace Calc.App.ViewModels;

public partial class CalculatorViewModel : ObservableObject
{
    private readonly ICalculatorService _calculatorService;
    private readonly INavigationService _navigationService;
    private bool _isNewEntry = true;
    private const double Tolerance = 0.0001;

    [ObservableProperty]
    private string _displayText = "0";

    [ObservableProperty]
    private double _lastResult;

    public CalculatorViewModel(ICalculatorService calculatorService, INavigationService navigationService)
    {
        _calculatorService = calculatorService;
        _navigationService = navigationService;
    }

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
        if (!string.IsNullOrEmpty(DisplayText) && !EndsWithOperator())
        {
            DisplayText += op;
            _isNewEntry = false;
        }
    }

    private bool EndsWithOperator()
    {
        return DisplayText.EndsWith("+") || DisplayText.EndsWith("-") || 
               DisplayText.EndsWith("*") || DisplayText.EndsWith("/");
    }

    [RelayCommand]
    private void AddDecimal()
    {
        // Split by operators to find the current segment being typed
        var parts = DisplayText.Split('+', '-', '*', '/');
        var currentPart = parts[^1];

        if (!currentPart.Contains("."))
        {
            DisplayText += ".";
            _isNewEntry = false;
        }
    }

    [RelayCommand]
    private void Backspace()
    {
        if (DisplayText.Length > 1)
            DisplayText = DisplayText[..^1];
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
}
