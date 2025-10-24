using FluentValidation;
using UrlShortner.Contracts;

namespace UrlShortner.Validators;

internal sealed class UrlShortnerRequestValidator : AbstractValidator<Request>
{
    private readonly Uri _appUri;
    public UrlShortnerRequestValidator(Uri appUri)
    {
        _appUri = appUri;
        RuleFor(r => r.OriginalUrl).
        Must(x => x is not null)
        .WithMessage("OriginalUrl is required");

        RuleFor(r => r.OriginalUrl).
        Length(1, 250)
        .WithMessage("OriginalUrl must be between 1 and 250 characters");

        RuleFor(r => r.OriginalUrl)
        .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
        .WithMessage("OriginalUrl must be a valid URL");

        RuleFor(r => r.OriginalUrl)
        .Must(url => !url.Contains(_appUri.Host))
        .WithMessage("Cannot shorten this app's domain");
        
    }
}