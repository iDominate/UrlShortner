namespace UrlShortner.Contracts;
internal sealed record Response(
string? OriginalUrl,
bool HasError,
string? ErrorMessage,
DateTime? ExpiresOn);