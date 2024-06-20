using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Infrastructure.Models;

public class NewsDetailModel
{
    public int errorCode { get; set; }
    public string errorMessage { get; set; }
    public DetailData data { get; set; }

}

public class DetailData
{
    public NewsDetails newsDetail { get; set; }
}
public class NewsDetails
{
    public string itemId { get; set; }
    public string resource { get; set; }
    public string title { get; set; }
    public string bodyText { get; set; }
    public string publishDate { get; set; }
    public string fullPath { get; set; }
    public string shortText { get; set; }
    public string imageUrl { get; set; }
    public CategoryDetails category { get; set; }
}

public class CategoryDetails
{
    public string categoryId { get; set; }
    public string title { get; set; }
    public string slug { get; set; }
}
