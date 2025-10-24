using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using UrlShortner.Contracts;
using UrlShortner.CQRS.ResolveUrl;

namespace UrlShortner.Endpoints;

public static class UrlResolverEndpoint
{
    public static IEndpointRouteBuilder RegisterEndpoint(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost("api/shorten/{url}", HandleRequest);

        return routeBuilder;
    }
    public static async Task<IResult> HandleRequest([AsParameters] Request request,
    ISender sender, HttpContext context, IValidator<Request> validator)
    {
        var validateReuqest = await validator.ValidateAsync(request);
        if(!validateReuqest.IsValid)
        {
            return Results.BadRequest(validateReuqest.Errors);
        }
        ResolveUrlCommandRequest resolveUrlCommandRequest = request;
        var response = await sender.Send(resolveUrlCommandRequest);
        if(response.HasError)
        {
            return Results.BadRequest(response.ErrorMessage);
        }
        return Results.Redirect(response.OriginalUrl);
    }

}