using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace KgNet88.Woden.Account.Api.Middleware;

public class ErrorMiddleware
{
    private readonly RequestDelegate _request;
    private readonly ProblemDetailsFactory _problemDetailsFactory;

    public ErrorMiddleware(RequestDelegate request, ProblemDetailsFactory problemDetailsFactory)
    {
        this._request = request;
        this._problemDetailsFactory = problemDetailsFactory;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await this._request(context);
        }
        catch (ValidationException exception)
        {
            var modelStateDictionary = new ModelStateDictionary();

            foreach (var failure in exception.Errors)
            {
                modelStateDictionary.AddModelError(failure.PropertyName, failure.ErrorMessage);
            }

            var problemDetails = this._problemDetailsFactory.CreateValidationProblemDetails(context, modelStateDictionary);

            context.Response.StatusCode = (int)problemDetails.Status!;

            await context.Response.WriteAsJsonAsync(problemDetails, options: null, contentType: "application/problem+json");
        }
    }
}
