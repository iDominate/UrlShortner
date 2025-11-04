namespace UrlShortner.Contracts;

public record ResolveRequest(string Code) : IRequest
;