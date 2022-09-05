namespace KgNet88.Woden.Account.Api.Middleware;

public static class ErrorOrExtensions
{
    public static async Task SendProblemDetailsAsync<T>(this ErrorOr<T> result, HttpContext context, ProblemDetailsFactory factory)
    {
        if (result.Errors.All(x => x.Type == ErrorType.Validation))
        {
            var modelStateDictionary = new ModelStateDictionary();

            foreach (var failure in result.Errors)
            {
                modelStateDictionary.AddModelError(failure.Code, failure.Description);
            }

            var problemDetails = factory.CreateValidationProblemDetails(context, modelStateDictionary);

            context.Response.StatusCode = (int)problemDetails.Status!;

            await context.Response.WriteAsJsonAsync(problemDetails, options: null, contentType: "application/problem+json");
        }
        else
        {
            int statusCode = result.FirstError.Type switch
            {
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Failure => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            var problemDetails = factory.CreateProblemDetails(context, statusCode, title: result.FirstError.Description);

            context.Response.StatusCode = (int)problemDetails.Status!;

            await context.Response.WriteAsJsonAsync(problemDetails, options: null, contentType: "application/problem+json");
        }
    }
}
