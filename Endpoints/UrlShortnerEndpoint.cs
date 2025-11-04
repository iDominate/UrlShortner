using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UrlShortner.Contracts;
using UrlShortner.Utils;

namespace UrlShortner.Endpoints;

public static class UrlShortnerEndpoint
{
    public static IEndpointRouteBuilder RegisterShortenEndpoint(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost("api/shorten/{url}", HandleRequest);

        return routeBuilder;
    }

    public static async Task<IResult> HandleRequest([AsParameters] ShortenRequest request,
    ISender sender,
    HttpContext context, [FromServices] IValidator<ShortenRequest> validator)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count != 0)
        {
            var errorsList = new List<string>();
            validationResult.Errors.ForEach(e => errorsList.Add(e.ErrorMessage));
            return Results.BadRequest(errorsList);
        }

        var shotenRequest = request.ToShortenUrlCommandRequest(
            context.Request.Host.ToString()
        );
        var shortenResult = await sender.Send(shotenRequest);
        if(shortenResult.HasError)
        {
            return Results.BadRequest(shortenResult.ErrorMessage);
        }
        return Results.Created(shortenResult.Response, new Response(shortenResult.Response, false,
        ErrorMessage: null,
         ExpiresOn: DateTime.UtcNow + TimeSpan.FromMinutes(30)));
    } 
}