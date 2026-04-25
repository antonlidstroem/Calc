using Calc.Core.Interfaces;
using Calc.Core.Models;
using Calc.Infrastructure.Services;
using System.Text.Json;

namespace Calc.Infrastructure.Repositories;

public class ArticleRepository : IArticleRepository
{
    private readonly FileService _fileService;
    private readonly string _userFilePath;
    private const string SystemFileName = "system_articles.json";
    private const string UserFileName = "user_articles.json";

    public ArticleRepository(FileService fileService)
    {
        _fileService = fileService;
        _userFilePath = Path.Combine(FileSystem.AppDataDirectory, UserFileName);
    }

    public async Task<List<Article>> GetSystemArticlesAsync()
    {
        try
        {
            var json = await _fileService.ReadAssetAsync(SystemFileName);
            var container = JsonSerializer.Deserialize<ArticleContainer>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return container?.Articles ?? new List<Article>();
        }
        catch
        {
            return new List<Article>();
        }
    }

    public async Task<List<Article>> GetUserArticlesAsync()
    {
        var container = await _fileService.ReadJsonAsync<ArticleContainer>(_userFilePath);
        return container?.Articles ?? new List<Article>();
    }

    public async Task SaveUserArticlesAsync(List<Article> articles)
    {
        var container = new ArticleContainer { Articles = articles };
        await _fileService.WriteJsonAsync(_userFilePath, container);
    }
}