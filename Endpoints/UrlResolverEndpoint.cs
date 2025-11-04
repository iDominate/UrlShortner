using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrlShortner.Contracts;
using UrlShortner.CQRS.ResolveUrl;
using UrlShortner.Utils;

namespace UrlShortner.Endpoints;

public static class UrlResolverEndpoint
{
    public static IEndpointRouteBuilder RegisterResolveEndpoint(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("api/resolve/{code}", HandleRequest);

        return routeBuilder;
    }
    public static async Task<IResult> HandleRequest([AsParameters] ResolveRequest request,
    ISender sender, HttpContext context, [FromServices] IValidator<ResolveRequest> validator)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count != 0)
        {
            var errorsList = new List<string>();
            validationResult.Errors.ForEach(e => errorsList.Add(e.ErrorMessage));
            return Results.BadRequest(errorsList);
        }
        ResolveUrlCommandRequest resolveUrlCommandRequest = request.
        ToResolveUrlCommandRequest(context.Connection.RemoteIpAddress!.ToString());
        var response = await sender.Send(resolveUrlCommandRequest);
        if(response.HasError)
        {
            return Results.BadRequest(response.ErrorMessage);
        }
        return Results.Redirect(response.OriginalUrl);
    }

}