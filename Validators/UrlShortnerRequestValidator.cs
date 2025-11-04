using FluentValidation;
using UrlShortner.Contracts;

namespace UrlShortner.Validators;

public class UrlShortnerRequestValidator : AbstractValidator<ShortenRequest>
{
    private readonly Uri _appUri;
    public UrlShortnerRequestValidator(Uri appUri)
    {
        _appUri = appUri;

        RuleFor(r => r.OriginalUrl).
        Length(10, 250)
        .WithMessage("OriginalUrl must be between 60 and 250 characters");

        RuleFor(r => r.OriginalUrl)
        .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _) is true)
        .WithMessage("OriginalUrl must be a valid URL");

        RuleFor(r => r.OriginalUrl)
        .Must(url => !url.Contains(_appUri.Host))
        .WithMessage("Cannot shorten this app's domain");        
    }
}