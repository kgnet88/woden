namespace KgNet88.Woden.Account.Api.Middleware;

public class ValidationExceptionMiddleware
{
    private readonly RequestDelegate _request;
    private readonly AbstractProblemDetailsFactory _problemDetailsFactory;

    public ValidationExceptionMiddleware(RequestDelegate request, AbstractProblemDetailsFactory problemDetailsFactory)
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
            var problemDetails = this._problemDetailsFactory.CreateValidationProblemDetails(context, exception.Errors.ToList());

            context.Response.StatusCode = 400;

            await context.Response.WriteAsJsonAsync(problemDetails, options: null, contentType: "application/problem+json");
        }
    }
}
