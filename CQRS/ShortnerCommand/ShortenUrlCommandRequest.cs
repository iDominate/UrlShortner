using MediatR;
using UrlShortner.Contracts;

namespace UrlShortner.CQRS.ShortnerCommand;

public record ShortenUrlCommandRequest(string OriginalUrl, string Host) : IRequest<ShortenUrlCommandResponse>
{
};
