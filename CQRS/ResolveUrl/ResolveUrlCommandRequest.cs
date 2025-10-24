using MediatR;
using UrlShortner.Contracts;

namespace UrlShortner.CQRS.ResolveUrl;

public record ResolveUrlCommandRequest(string IpAddress, string ShortenedUrl) : IRequest<ResolveUrlCommandResponse>
{
    public static implicit operator ResolveUrlCommandRequest(Request request)
    {
        return new ResolveUrlCommandRequest(request.IPAddress,request.OriginalUrl);
    }
};

