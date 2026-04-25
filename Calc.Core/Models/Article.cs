namespace Calc.Core.Models;

public class Article
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Prayer { get; set; } = string.Empty;
}

public class ArticleContainer
{
    public List<Article> Articles { get; set; } = new();
}