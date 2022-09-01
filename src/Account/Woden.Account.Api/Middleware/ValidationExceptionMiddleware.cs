namespace Goedde88.Woden.Account.Api.Middleware;

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
            context.Response.StatusCode = 400;
            var messages = exception.Errors.Select(x => x.ErrorMessage).ToList();
            var validationFailureResponse = new ValidationFailureResponse
            {
                Errors = messages
            };
            await context.Response.WriteAsJsonAsync(validationFailureResponse);
        }
    }
}
