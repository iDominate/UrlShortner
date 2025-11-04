using System.Net;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using UrlShortner.Contracts;

namespace UrlShortner.Middlewares;

class RateLimiterMiddleware
{
    private readonly RequestDelegate _next;
    private IConnectionMultiplexer _redisConnection;
    private static int LimitPerTimeFrame => 30;
    private static TimeSpan TimeFrame => TimeSpan.FromMinutes(1);
    public RateLimiterMiddleware(IConnectionMultiplexer redisConnection
    , RequestDelegate next)
    {
        _next = next;
        _redisConnection = redisConnection;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            var redisDatabase = _redisConnection.GetDatabase(1);
            var key = GetIPAddressKey(context);
            var storedLimitValue = await GetIPAddressCountAsync(redisDatabase, context, key);
            if (storedLimitValue is null)
            {
                await GenerateNewIpAddressCountEntryAsync(redisDatabase, key);
                await _next(context);
            }
            var remainingTime = await redisDatabase.KeyTimeToLiveAsync(key);
            var retryAfter = (int)(remainingTime?.TotalSeconds ?? TimeFrame.TotalSeconds);
            if (storedLimitValue > LimitPerTimeFrame)
            {
               GenerateTooManyRequestsResponse(context, retryAfter);
               return;
            }
            else
            {
                await IncrementExistingValueAsync(redisDatabase, key);
            }
            await _next(context);

        }
        catch (RedisException ex)
        {
            await GenerateErrorResponseAsync(context, ex.Message);
        }

    }

    public async Task<int?> GetIPAddressCountAsync(IDatabase database, HttpContext context, string key)
    {
        var redisDatabase = _redisConnection.GetDatabase(1);
        var ipAddress = context.Connection.RemoteIpAddress!.ToString();
        int.TryParse(await redisDatabase.StringGetAsync(key), out int storedLimitValue);
        return storedLimitValue;
    }
    public string GetIPAddressKey(HttpContext context)
    {
        return $"rate_limit:{context.Connection.RemoteIpAddress!.ToString()}";
    }

    public async Task GenerateNewIpAddressCountEntryAsync(IDatabase database, string key)
    {
        await database.StringSetAsync(key, "1", expiry: TimeFrame);
    }

    public void GenerateTooManyRequestsResponse(HttpContext context, int retryAfter)
    {
        context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
        context.Response.Headers.RetryAfter = retryAfter.ToString();
        return;
    }

    public async Task IncrementExistingValueAsync(IDatabase database, string key)
    {
        await database.StringIncrementAsync(key);
    }

    public async Task GenerateErrorResponseAsync(HttpContext context, string message)
    {
        await context.Response.
        WriteAsJsonAsync(
        new Response(null!, true, message, null));
        return;
    }
}
