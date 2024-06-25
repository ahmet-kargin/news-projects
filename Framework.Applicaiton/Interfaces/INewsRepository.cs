using Framework.Domain.Entities;

namespace Framework.Application.Interfaces;

// INewsRepository adında bir arayüz tanımlıyoruz..
public interface INewsRepository
{
    Task<IEnumerable<News>> GetAllAsync(); // Tüm haberleri almak için bir metot tanımlıyoruz.
    Task<NewsDetail> GetByIdAsync(string id); // Bir haberin detaylarını almak için bir metot tanımlıyoruz.
    Task<List<string>> GetAllCategories(); 
}
