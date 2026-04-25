using Calc.Core.Models;

namespace Calc.Core.Interfaces;

public interface IArticleRepository
{
    Task<List<Article>> GetSystemArticlesAsync();
    Task<List<Article>> GetUserArticlesAsync();
    Task SaveUserArticlesAsync(List<Article> articles);
}