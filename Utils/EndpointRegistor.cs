using UrlShortner.Endpoints;

namespace UrlShortner.Utils;

internal static class EndpointRegistor
{
    public static IEndpointRouteBuilder RegisterEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.RegisterShortenEndpoint();
        builder.RegisterResolveEndpoint();
        return builder; 
    }
}