using Framework.Application.Interfaces;
using Framework.Infrastructure.Models;
using Framework.Infrastructure.Repository;
using Framework.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.Configure<NewsApiUrls>(builder.Configuration.GetSection("NewsApiUrls"));

// Dependency Injection registration
builder.Services.AddHttpClient(); // HttpClient'i ekleyin

builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddScoped<NewsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=News}/{action=Index}/{id?}");

app.Run();
