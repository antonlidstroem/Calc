using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Calc.Core.Interfaces;
using Calc.Core.Models;
using System.Collections.ObjectModel;

namespace Calc.App.ViewModels;

public partial class ArticlesViewModel : ObservableObject
{
    private readonly IArticleRepository _repository;
    private readonly Services.INavigationService _navigationService;

    public ObservableCollection<Article> AllArticles { get; } = new();

    public ArticlesViewModel(IArticleRepository repository, Services.INavigationService navigationService)
    {
        _repository = repository;
        _navigationService = navigationService;
    }

    [RelayCommand]
    public async Task LoadArticlesAsync()
    {
        AllArticles.Clear();
        
        var system = await _repository.GetSystemArticlesAsync();
        var user = await _repository.GetUserArticlesAsync();

        foreach (var a in system.Concat(user))
            AllArticles.Add(a);
    }

    [RelayCommand]
    private async Task SelectArticle(Article article)
    {
        await _navigationService.GoToAsync(nameof(Views.ArticleDetailView), 
            new Dictionary<string, object> { { "Article", article } });
    }
}
