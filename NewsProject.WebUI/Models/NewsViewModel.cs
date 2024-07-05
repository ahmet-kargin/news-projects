using Framework.Domain.Entities;
using PagedList.Core;

namespace NewsProject.Models;

public class NewsViewModel
{
    public IPagedList<News> News { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalNewsCount { get; set; }
    public List<string> Category { get; set; }
    public string Keyword { get; set; }
}
