using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Entities;

public class NewsDetail
{
    public string ItemId { get; set; }
    public string Title { get; set; }
    public string BodyText { get; set; }
    public string PublishDate { get; set; }
    public string FullPath { get; set; }
    public string ShortText { get; set; }
    public string ImageUrl { get; set; }
    public string Category { get; set; }
}
