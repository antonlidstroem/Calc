using Calc.Maui.ViewModels;

namespace Calc.Maui.Views;

public partial class SettingsView : ContentPage
{
    public SettingsView(SettingsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
