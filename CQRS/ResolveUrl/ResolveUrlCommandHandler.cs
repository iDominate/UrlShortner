using MediatR;
using StackExchange.Redis;

namespace UrlShortner.CQRS.ResolveUrl;

public class ResolveUrlCommandHandler(IConnectionMultiplexer connection) : IRequestHandler<ResolveUrlCommandRequest, ResolveUrlCommandResponse>
{
    private IConnectionMultiplexer Connection => connection;

    public Task<ResolveUrlCommandResponse> Handle(ResolveUrlCommandRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var redisDatabase = Connection.GetDatabase(0);
            var UrlKeyValue = redisDatabase.StringGet(request.ShortenedUrl);
            if (UrlKeyValue.IsNull)
            {
                return Task.FromResult(ResolveUrlCommandResponse.Create(
                    errorMessage: "Url Not Found",
                    hasError: true
                ));
            } else
            {
                return Task.FromResult(ResolveUrlCommandResponse.Create(UrlKeyValue.ToString()));
            }
        } catch (RedisException redisException)
        {
            return Task.FromResult(ResolveUrlCommandResponse.
            Create(hasError: true, errorMessage: redisException.Message));
        }
        
    }
}