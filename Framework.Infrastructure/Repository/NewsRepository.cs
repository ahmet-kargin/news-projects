using Framework.Application.Interfaces;
using Framework.Domain.Entities;
using Framework.Infrastructure.Models;
using Newtonsoft.Json;

namespace Framework.Infrastructure.Repository;

// NewsRepository sınıfı INewsRepository arayüzünü implemente ediyor.
public class NewsRepository : INewsRepository
{
    private readonly HttpClient _httpClient; // HttpClient nesnesi kullanarak haberleri alacağız.
    private readonly string _newsJsonUrl = "https://www.turkmedya.com.tr/anasayfa.json"; // Ana sayfa verilerinin JSON URL'si.
    private readonly string _newsDetailJsonUrl = "https://www.turkmedya.com.tr/detay.json"; // Haber detay verilerinin JSON URL'si.

    // Constructor, HttpClient nesnesini enjekte eder.
    public NewsRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Tüm haberleri almak için bu metot kullanılır.
    public async Task<IEnumerable<News>> GetAllAsync()
    {
        var response = await _httpClient.GetAsync(_newsJsonUrl); // JSON verilerini almak için HTTP GET isteği yapılır.
        response.EnsureSuccessStatusCode(); // HTTP isteği başarılı ise devam edilir.
        var jsonString = await response.Content.ReadAsStringAsync(); // JSON verileri string formatına dönüştürülür.

        var jsonData = JsonConvert.DeserializeObject<NewsModel>(jsonString); // JSON verileri NewsModel tipine dönüştürülür.
        var newsList = new List<News>(); // Haber listesi oluşturulur.

        // JSON verileri döngü ile işlenir ve News sınıfı nesneleri oluşturulur.
        if (jsonData != null && jsonData.data != null)
        {
            foreach (var data in jsonData.data)
            {
                foreach (var itemList in data.itemList)
                {
                    var news = new News
                    {
                        ItemId = itemList.itemId,
                        Title = itemList.title,
                        ShortText = itemList.shortText,
                        PublishDate = itemList.publishDate,
                        FullPath = itemList.fullPath,
                        ImageUrl = itemList.imageUrl,
                        Category = itemList.category != null ? itemList.category.title : "" // Null kontrolü ekleniyor
                    };
                    newsList.Add(news); // Oluşturulan haber nesnesi listeye eklenir.
                }
            }
        }
        return newsList; // Oluşturulan haber listesi geri döndürülür.
    }

    // Belirli bir haberin detaylarını almak için bu metot kullanılır.
    public async Task<NewsDetail> GetByIdAsync(string id)
    {
        var response = await _httpClient.GetAsync(_newsDetailJsonUrl); // JSON verilerini almak için HTTP GET isteği yapılır.
        response.EnsureSuccessStatusCode(); // HTTP isteği başarılı ise devam edilir.
        var jsonString = await response.Content.ReadAsStringAsync(); // JSON verileri string formatına dönüştürülür.

        var jsonData = JsonConvert.DeserializeObject<NewsDetailModel>(jsonString); // JSON verileri NewsDetailModel tipine dönüştürülür.
        var newsItem = jsonData.data.newsDetail; // Haber detayları alınır.

        // Verilen ID ile eşleşen haber detayları bulunursa, bu haber detayları geri döndürülür.
        if (newsItem.itemId == id)
        {
            return new NewsDetail
            {
                ItemId = newsItem.itemId,
                Title = newsItem.title,
                BodyText = newsItem.bodyText,
                PublishDate = newsItem.publishDate,
                FullPath = newsItem.fullPath,
                ShortText = newsItem.shortText,
                ImageUrl = newsItem.imageUrl,
                Category = newsItem.category.title
            };
        }

        return null; // ID ile eşleşen haber detayı bulunamazsa, null döndürülür.
    }
}
