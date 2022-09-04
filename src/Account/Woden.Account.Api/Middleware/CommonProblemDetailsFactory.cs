using System.Diagnostics;

using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace KgNet88.Woden.Account.Api.Middleware;
public class CommonProblemDetailsFactory : AbstractProblemDetailsFactory
{
    private readonly ApiBehaviorOptions _options;

    public CommonProblemDetailsFactory(IOptions<ApiBehaviorOptions> options)
    {
        this._options = options?.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public override MyValidationProblemDetails CreateValidationProblemDetails(
        HttpContext httpContext,
        List<ValidationFailure> failures,
        int statusCode = 400,
        string? instance = null)
    {
        var problemDetails = new MyValidationProblemDetails
        {
            Status = statusCode,
            Title = "Validation Error",

            Detail = "One or more errors occured!",
            Instance = instance ?? httpContext.Request.Path,
            Errors = new()
        };

        foreach (var failure in failures)
        {
            if (problemDetails.Errors.TryGetValue(failure.PropertyName, out var errorList))
            {
                errorList!.Add(failure.ErrorMessage);
            }
            else
            {
                problemDetails.Errors.Add(failure.PropertyName, new List<string> { failure.ErrorMessage });
            }
        }

        this.ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode);

        return problemDetails;
    }

    private void ApplyProblemDetailsDefaults(HttpContext httpContext, ProblemDetails problemDetails, int statusCode)
    {
        problemDetails.Status ??= statusCode;

        if (this._options.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorData))
        {
            problemDetails.Title ??= clientErrorData.Title;
            problemDetails.Type ??= clientErrorData.Link;
        }

        string? traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;

        if (traceId != null)
        {
            problemDetails.Extensions["traceId"] = traceId;
        }
    }
}
