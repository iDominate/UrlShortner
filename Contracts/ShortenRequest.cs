namespace UrlShortner.Contracts;

public record ShortenRequest(string OriginalUrl) : IRequest;