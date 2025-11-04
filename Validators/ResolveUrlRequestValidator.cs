using FluentValidation;
using UrlShortner.Contracts;

namespace UrlShortner.Validators;

public class ResolveUrlRequestValidator : AbstractValidator<ResolveRequest>
{
    public ResolveUrlRequestValidator()
    {
        
    }    
}