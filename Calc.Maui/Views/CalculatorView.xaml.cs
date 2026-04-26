using Calc.Maui.ViewModels;

namespace Calc.Maui.Views;

public partial class CalculatorView : ContentPage
{
    public CalculatorView(CalculatorViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
