using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UrlShortner.Contracts;

namespace UrlShortner.Endpoints;

public static class UrlShortnerEndpoint
{
    public static IEndpointRouteBuilder RegisterEndpoint(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost("api/shorten/{url}", HandleRequest);

        return routeBuilder;
    }

    public static IResult HandleRequest([AsParameters] Request request)
    {
        return Results.Ok();
    }    
}