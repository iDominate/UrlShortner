using UrlShortner.CQRS.Common;

namespace UrlShortner.CQRS.ShortnerCommand;

public record ShortenUrlCommandResponse : IUrlResponse
{
    public string Response { get; private set; }
    public bool HasError { get; private set; }
    public string? ErrorMessage { get; private set; }


    private ShortenUrlCommandResponse(string response, bool hasError, string? errorMessage)
    {
        Response = response;
        HasError = hasError;
        ErrorMessage = errorMessage;
    }
    
    public static ShortenUrlCommandResponse Create(string? response, bool hasError, string? errorMessage)
    {
        return new ShortenUrlCommandResponse(response!, hasError, errorMessage);
    }
}