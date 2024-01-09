using categories_api.CacheService;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddStackExchangeRedisCache(o =>
{
    o.Configuration = builder.Configuration["RedisCacheUrl"];
});
builder.Services.AddSingleton<RedisCacheService>();
var app = builder.Build();

app.MapGet("/categories", (RedisCacheService redisCacheService) =>
{
    var categoriesKey = "Redis_Categories";

    var categories = redisCacheService.GetCashedData<string[]>(categoriesKey);
    if (categories is null)
    {
        categories = new string[] { "one", "two", "three", "four" };
        redisCacheService.AddCashedData(categoriesKey, categories, TimeSpan.FromSeconds(60));
    }

    return categories;
});

app.Run();
