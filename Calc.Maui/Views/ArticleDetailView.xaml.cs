using Calc.Core.Models;

namespace Calc.App.Views;

// "Article" är namnet på parametern vi skickar i NavigationService
[QueryProperty(nameof(SelectedArticle), "Article")]
public partial class ArticleDetailView : ContentPage
{
    private Article _selectedArticle;
    public Article SelectedArticle
    {
        get => _selectedArticle;
        set
        {
            _selectedArticle = value;
            OnPropertyChanged();
            // Vi sätter BindingContext till artikeln direkt för enkelhetens skull
            BindingContext = _selectedArticle;
        }
    }

    public ArticleDetailView()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        // Här kan du lägga till logik om något ska hända när man landar på sidan
    }
}
