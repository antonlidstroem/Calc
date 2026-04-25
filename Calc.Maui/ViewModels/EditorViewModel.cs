using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Calc.Core.Models;
using Calc.Core.Interfaces;
using System.Collections.ObjectModel;
using Calc.Infrastructure.Services;

namespace Calc.App.ViewModels;

public partial class EditorViewModel : BaseSecurityViewModel
{
    private readonly IArticleRepository _repository;

    [ObservableProperty]
    private ObservableCollection<Article> _userArticles = new();

    [ObservableProperty]
    private Article _currentArticle = new();

    public EditorViewModel(IArticleRepository repository, SecurityService securityService) 
        : base(securityService)
    {
        _repository = repository;
        LoadUserArticles();
    }

    private async void LoadUserArticles()
    {
        var articles = await _repository.GetUserArticlesAsync();
        UserArticles = new ObservableCollection<Article>(articles);
    }

    [RelayCommand]
    private async Task SaveArticle()
    {
        if (string.IsNullOrWhiteSpace(CurrentArticle.Title)) return;

        UserArticles.Add(CurrentArticle);
        await _repository.SaveUserArticlesAsync(UserArticles.ToList());
        
        CurrentArticle = new Article(); // Nollställ formulär
        SecurityService.ResetTimer();   // Säkerhets-reset vid aktivitet
    }

    [RelayCommand]
    private async Task DeleteArticle(Article article)
    {
        bool answer = await Shell.Current.DisplayAlert("Bekräfta", "Vill du radera artikeln?", "Ja", "Nej");
        if (answer)
        {
            UserArticles.Remove(article);
            await _repository.SaveUserArticlesAsync(UserArticles.ToList());
        }
    }

    [RelayCommand]
    private void ClearForm() => CurrentArticle = new Article();
}
