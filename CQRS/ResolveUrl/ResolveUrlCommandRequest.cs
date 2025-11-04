using MediatR;
using UrlShortner.Contracts;

namespace UrlShortner.CQRS.ResolveUrl;

public record ResolveUrlCommandRequest(string IpAddress, string ShortenedUrl) : IRequest<ResolveUrlCommandResponse>
{
};

