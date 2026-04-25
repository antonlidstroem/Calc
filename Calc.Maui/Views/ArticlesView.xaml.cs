using Calc.App.ViewModels;

namespace Calc.App.Views;

public partial class ArticlesView : ContentPage
{
    private readonly ArticlesViewModel _viewModel;

    public ArticlesView(ArticlesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // Trigga laddning av artiklar när vyn visas
        await _viewModel.LoadArticlesCommand.ExecuteAsync(null);
    }
}
