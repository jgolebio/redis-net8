var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/categories", () => new string[] { "one", "two", "three", "four" });

app.Run();
