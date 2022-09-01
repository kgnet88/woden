namespace KgNet88.Woden.Account.Api.Middleware;

public class ValidationExceptionMiddleware
{
    private readonly RequestDelegate _request;

    public ValidationExceptionMiddleware(RequestDelegate request)
    {
        this._request = request;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await this._request(context);
        }
        catch (ValidationException exception)
        {
            await context.Response.SendErrorsAsync(exception.Errors.ToList());
        }
    }
}
