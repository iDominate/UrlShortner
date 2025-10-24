using UrlShortner.CQRS.Common;

namespace UrlShortner.CQRS.ResolveUrl;

public class ResolveUrlCommandFactory : IUrlResponseFactory
{
    IUrlResponse IUrlResponseFactory.CreateResponse(string? response, bool HasError, string? ErrorMessage)
    {
        return ResolveUrlCommandResponse.Create(response!);
    }
}