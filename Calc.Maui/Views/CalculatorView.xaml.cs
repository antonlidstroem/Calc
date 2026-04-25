using Calc.App.ViewModels;

namespace Calc.App.Views;

public partial class CalculatorView : ContentPage
{
    public CalculatorView(CalculatorViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
