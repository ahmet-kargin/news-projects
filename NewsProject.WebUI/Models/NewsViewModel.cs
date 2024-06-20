using Framework.Domain.Entities;

namespace NewsProject.Models;

public class NewsViewModel
{
    public IEnumerable<News> News { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalNewsCount { get; set; }
    public List<string> Category { get; set; }
    public string Keyword { get; set; }
    public string SelectedCategory { get; set; }
}
