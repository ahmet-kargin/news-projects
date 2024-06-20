using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Infrastructure.Models;

public class NewsModel
{
    public int errorCode { get; set; }
    public string errorMessage { get; set; }
    public List<Data> data { get; set; }
    
}

public class Data
{
    public string repeatType { get; set; }
    public List<ItemList> itemList { get; set; }
}

public class ItemList
{
    public string itemId { get; set; }
    public string title { get; set; }
    public string shortText { get; set; }
    public string publishDate { get; set; }
    public string fullPath { get; set; }
    public string imageUrl { get; set; }
    public Category category { get; set; }

}

public class Category
{
    public string categoryId { get; set; }
    public string title { get; set; }
    public string slug { get; set; }
}
