using FluentValidation.Results;

namespace KgNet88.Woden.Account.Api.Middleware;
public abstract class AbstractProblemDetailsFactory
{
    public abstract MyValidationProblemDetails CreateValidationProblemDetails(
        HttpContext httpContext,
        List<ValidationFailure> failures,
        int statusCode = 400,
        string? instance = null);
}