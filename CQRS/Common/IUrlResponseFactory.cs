namespace UrlShortner.CQRS.Common;

interface IUrlResponseFactory
{
    abstract IUrlResponse CreateResponse(string? response, bool HasError, string? ErrorMessage);
}