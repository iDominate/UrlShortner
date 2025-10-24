using UrlShortner.CQRS.Common;

namespace UrlShortner.CQRS.ResolveUrl;

public class ResolveUrlCommandResponse : IUrlResponse
{
    public string OriginalUrl { get; private set; }
    public bool HasError { get; private set; }
    public string? ErrorMessage { get; private set; }


    private ResolveUrlCommandResponse(string? originalUrl = null, bool hasError = false, string? errorMessage = null
    )
    {
        OriginalUrl = originalUrl!;
        HasError = hasError;
        ErrorMessage = errorMessage;

    }

    public static ResolveUrlCommandResponse Create(string? originalUrl = null!, string? errorMessage = null!, bool hasError = false)
    {
        return new ResolveUrlCommandResponse(originalUrl, hasError, errorMessage);
    }    
}