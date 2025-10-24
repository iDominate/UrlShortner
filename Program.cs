using StackExchange.Redis;
using UrlShortner.Middlewares;
using UrlShortner.Services;
using UrlShortner.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSingleton<IConnectionMultiplexer>(
    sp =>
    {
        var config = builder.Configuration.GetConnectionString("Redis");
        return ConnectionMultiplexer.Connect(config!);
    }
);
builder.Services.RegisterUrlDomainService(builder.Configuration);
var app = builder.Build();
app.UseMiddleware<RateLimiterMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.RegisterEndpoints();
app.UseHttpsRedirection();


app.Run();