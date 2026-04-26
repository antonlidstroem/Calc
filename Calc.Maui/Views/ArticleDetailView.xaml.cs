using Calc.Maui.ViewModels;

namespace Calc.Maui.Views;

public partial class ArticlesView : ContentPage
{
    private readonly ArticlesViewModel _viewModel;

    public ArticlesView(ArticlesViewModel viewModel)
    {
        InitializeComponent();

        // Sätter BindingContext till den inskickade viewmodel-instansen
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Trigga laddning av både system-artiklar och krypterade användar-artiklar
        if (_viewModel.LoadArticlesCommand.CanExecute(null))
        {
            await _viewModel.LoadArticlesCommand.ExecuteAsync(null);
        }
    }
}