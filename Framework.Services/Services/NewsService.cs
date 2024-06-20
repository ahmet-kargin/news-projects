﻿using Framework.Application.Interfaces;
using Framework.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Services.Services;


// NewsService sınıfı, iş mantığını uygulayan servis sınıfını temsil eder.
public class NewsService
{
    private readonly INewsRepository _newsRepository; // INewsRepository arayüzü üzerinden haber verilerine erişim sağlanır.

    // Constructor, bir INewsRepository örneği alır.
    public NewsService(INewsRepository newsRepository)
    {
        _newsRepository = newsRepository; // Dependency injection ile gelen INewsRepository örneği atanır.
    }

    // Verilen filtrelerle haberlerin bir sayfa boyutunda bir kısmını almak için kullanılır.
    public async Task<IEnumerable<News>> GetNewsAsync(int pageNumber, int pageSize, string category = null, string keyword = null)
    {
        var newsList = await _newsRepository.GetAllAsync(); // Tüm haberleri alır.

        // Kategori veya anahtar kelimeye göre haberleri filtreler.
        if (newsList != null)
        {
            if (!string.IsNullOrEmpty(category))
            {
                newsList = newsList.Where(n => n.Category.Equals(category, StringComparison.OrdinalIgnoreCase)); // Kategoriye göre filtreleme yapar.
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                newsList = newsList.Where(n => n.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase)); // Anahtar kelimeye göre filtreleme yapar.
            }
        }

        // Sayfalama yaparak sadece belirli bir sayfa boyutundaki haberleri döndürür.
        return newsList.Skip((pageNumber - 1) * pageSize).Take(pageSize);
    }

    // Verilen filtrelerle toplam haber sayısını almak için kullanılır.
    public async Task<int> GetTotalCountAsync(string category = null, string keyword = null)
    {
        var newsList = await _newsRepository.GetAllAsync(); // Tüm haberleri alır.

        // Kategori veya anahtar kelimeye göre haberleri filtreler.
        if (!string.IsNullOrEmpty(category))
        {
            newsList = newsList.Where(n => n.Category.Equals(category, StringComparison.OrdinalIgnoreCase)); // Kategoriye göre filtreleme yapar.
        }
        if (!string.IsNullOrEmpty(keyword))
        {
            newsList = newsList.Where(n => n.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase)); // Anahtar kelimeye göre filtreleme yapar.
        }

        return newsList.Count(); // Filtrelenen haberlerin sayısını döndürür.
    }

    // Belirli bir haberin detaylarını ID'ye göre almak için kullanılır.
    public async Task<NewsDetail> GetNewsDetailByIdAsync(string id)
    {
        return await _newsRepository.GetByIdAsync(id); // ID'ye göre haber detaylarını döndürür.
    }
}
