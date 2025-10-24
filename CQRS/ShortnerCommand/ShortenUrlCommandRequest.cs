using MediatR;
using UrlShortner.Contracts;

namespace UrlShortner.CQRS.ShortnerCommand;

public record ShortenUrlCommandRequest(string OriginalUrl) : IRequest<ShortenUrlCommandResponse>
{
    public static implicit operator ShortenUrlCommandRequest(Request request)
    {
        return new ShortenUrlCommandRequest(request.OriginalUrl);
    }
};
