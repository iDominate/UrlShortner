using UrlShortner.CQRS.Common;

namespace UrlShortner.CQRS.ShortnerCommand;

public class ShortenUrlCommandResponseFactory : IUrlResponseFactory
{
    IUrlResponse IUrlResponseFactory.CreateResponse(string? response, bool hasError, string? errorMessage)
    {
        return ShortenUrlCommandResponse.Create(response, hasError, errorMessage);
    }
}