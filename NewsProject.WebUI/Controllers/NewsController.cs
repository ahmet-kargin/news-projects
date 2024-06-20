using Microsoft.AspNetCore.Mvc;
using Framework.Services.Services;
using NewsProject.Models;

namespace NewsProject.Controllers;

// NewsController sınıfı, haberle ilgili HTTP isteklerini işleyen MVC controller'ını temsil eder.
public class NewsController : Controller
{
    private readonly NewsService _newsService; // NewsService sınıfından bir örnek.

    // Constructor, bir NewsService örneği alır.
    public NewsController(NewsService newsService)
    {
        _newsService = newsService; // Dependency injection ile gelen NewsService örneği atanır.
    }

    // Index action method, haberlerin listelendiği ana sayfayı oluşturur.
    public async Task<IActionResult> Index(int pageNumber = 1, string category = null, string keyword = null)
    {
        int pageSize = 5; // Sayfa başına gösterilecek haber sayısı.

        // Verilen filtrelerle haberleri alır.
        var news = await _newsService.GetNewsAsync(pageNumber, pageSize, category, keyword);

        // Verilen filtrelerle toplam haber sayısını alır.
        var totalNewsCount = await _newsService.GetTotalCountAsync(category, keyword);

        // Alınan haberlerden farklı kategorileri alır.
        var distinctCategories = news.Select(n => n.Category).Distinct().ToList();

        // NewsViewModel oluşturulur ve gerekli veriler atanır.
        var model = new NewsViewModel
        {
            News = news,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalNewsCount = totalNewsCount,
            Category = distinctCategories,
            Keyword = keyword,
            SelectedCategory = category // Seçilen kategori değişkeni atanır.
        };

        return View(model); // Model ile birlikte Index view'ına yönlendirme yapılır.
    }

    // Details action method, belirli bir haberin detaylarını gösterir.
    public async Task<IActionResult> Details(string id)
    {
        // Verilen ID'ye göre haber detaylarını alır.
        var newsDetail = await _newsService.GetNewsDetailByIdAsync(id);

        // Haber detayları bulunamazsa, ana sayfaya yönlendirme yapılır.
        if (newsDetail == null)
        {
            return RedirectToAction("Index", "News");
        }

        // NewsDetailViewModel oluşturulur ve gerekli veriler atanır.
        var model = new NewsDetailViewModel
        {
            Title = newsDetail.Title,
            ImageUrl = newsDetail.ImageUrl,
            PublishDate = newsDetail.PublishDate,
            BodyText = newsDetail.BodyText,
            FullPath = newsDetail.FullPath
        };

        return View(model); // Model ile birlikte Details view'ına yönlendirme yapılır.
    }
}

