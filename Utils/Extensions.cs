using UrlShortner.Contracts;
using UrlShortner.CQRS.ResolveUrl;
using UrlShortner.CQRS.ShortnerCommand;

namespace UrlShortner.Utils;

public static class Extensions
{
    public static ShortenUrlCommandRequest ToShortenUrlCommandRequest(
        this ShortenRequest request,
        string host)
    {
        return new ShortenUrlCommandRequest(request.OriginalUrl, host);
    }
    public static ResolveUrlCommandRequest ToResolveUrlCommandRequest(
        this ResolveRequest request, string ipAddress)
    {
        return new ResolveUrlCommandRequest(ipAddress, request.Code);
    }
            
}