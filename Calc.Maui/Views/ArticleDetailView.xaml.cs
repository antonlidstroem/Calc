using Calc.Core.Models;

namespace Calc.Maui.Views;

[QueryProperty(nameof(SelectedArticle), "Article")]
public partial class ArticleDetailView : ContentPage
{
    private Article? _selectedArticle;
    public Article? SelectedArticle
    {
        get => _selectedArticle;
        set
        {
            _selectedArticle = value;
            OnPropertyChanged();
            BindingContext = _selectedArticle;
        }
    }

    public ArticleDetailView()
    {
        InitializeComponent();
    }
}