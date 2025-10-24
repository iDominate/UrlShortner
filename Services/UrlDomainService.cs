namespace UrlShortner.Services;

internal static class UrlDomainService
{
    public static IServiceCollection RegisterUrlDomainService(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        var appUrl = new Uri(configuration.GetSection("AppUrl").Value!);
        services.AddSingleton(appUrl);
        return services;
    }    
}