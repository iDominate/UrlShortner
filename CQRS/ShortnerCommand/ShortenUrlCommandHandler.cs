using MediatR;
using StackExchange.Redis;

namespace UrlShortner.CQRS.ShortnerCommand;

public class ShortenUrlCommandHandler :
IRequestHandler<ShortenUrlCommandRequest, ShortenUrlCommandResponse>
{
    private IConnectionMultiplexer _redisConnection;
    public ShortenUrlCommandHandler(IConnectionMultiplexer redisConnection)
    {
        _redisConnection = redisConnection;
    }
    public async Task<ShortenUrlCommandResponse> Handle(ShortenUrlCommandRequest request, CancellationToken cancellationToken)
    {
        var UrlGuidIdnetifier = Guid.NewGuid();
        try
        {
            var redisDatabase = _redisConnection.GetDatabase(0);
            await redisDatabase.StringSetAsync(UrlGuidIdnetifier.ToString(),
             request.OriginalUrl,
              expiry: TimeSpan.FromMinutes(30));
            return ShortenUrlCommandResponse.Create($"http://{request.Host}/api/resolve/{UrlGuidIdnetifier}", false, null);
            
        } catch(RedisException redisException)
        {
            return ShortenUrlCommandResponse.Create(null, true, redisException.Message);
        }
        
    }
}