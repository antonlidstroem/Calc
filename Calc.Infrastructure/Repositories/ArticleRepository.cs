using Calc.Core.Interfaces;
using Calc.Core.Models;
using Calc.Infrastructure.Services;
using System.Text.Json;

namespace Calc.Infrastructure.Repositories;

public class ArticleRepository : IArticleRepository
{
    private readonly FileService _fileService;
    private readonly SecurityService _securityService;
    private readonly string _userFilePath;
    private const string SystemFileName = "system_articles.json";
    private const string UserFileName = "user_articles.enc"; // Vi byter ändelse för att markera kryptering

    public ArticleRepository(FileService fileService, SecurityService securityService)
    {
        _fileService = fileService;
        _securityService = securityService;
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
        catch { return new List<Article>(); }
    }

    public async Task<List<Article>> GetUserArticlesAsync()
    {
        string? key = _securityService.CurrentSessionKey;
        if (string.IsNullOrEmpty(key)) return new List<Article>();

        var container = await _fileService.ReadEncryptedJsonAsync<ArticleContainer>(_userFilePath, key);
        return container?.Articles ?? new List<Article>();
    }

    public async Task SaveUserArticlesAsync(List<Article> articles)
    {
        string? key = _securityService.CurrentSessionKey;
        if (string.IsNullOrEmpty(key)) return;

        var container = new ArticleContainer { Articles = articles };
        await _fileService.WriteEncryptedJsonAsync(_userFilePath, container, key);
    }
}
