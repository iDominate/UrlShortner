using UrlShortner.CQRS.ResolveUrl;

namespace UrlShortner.Contracts;
public sealed record Request(string IPAddress, string OriginalUrl,DateTime ExpiresOn);