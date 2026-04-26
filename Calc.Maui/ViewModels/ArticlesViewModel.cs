using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Calc.Core.Interfaces;
using Calc.Core.Models;
using Calc.Infrastructure.Services;
using System.Collections.ObjectModel;
using Microsoft.Maui.ApplicationModel.DataTransfer;

namespace Calc.App.ViewModels;

public partial class ArticlesViewModel : BaseSecurityViewModel
{
    private readonly IArticleRepository _repository;
    private readonly Services.INavigationService _navigationService;

    public ObservableCollection<Article> AllArticles { get; } = new();

    public ArticlesViewModel(IArticleRepository repository, Services.INavigationService navigationService, SecurityService securityService) 
        : base(securityService)
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
            
        SecurityService.ResetTimer();
    }

    [RelayCommand]
    private async Task SelectArticle(Article article)
    {
        await _navigationService.GoToAsync(nameof(Views.ArticleDetailView), 
            new Dictionary<string, object> { { "Article", article } });
    }

    [RelayCommand]
    private async Task ShareApp()
    {
        await Share.Default.RequestAsync(new ShareTextRequest
        {
            Uri = "https://yourstealthapp.com/download",
            Title = "Dela Calc App"
        });
    }
}
