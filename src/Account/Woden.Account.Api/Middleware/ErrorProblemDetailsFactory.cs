namespace KgNet88.Woden.Account.Api.Middleware;

/// <summary>
/// Factory to produce <see cref="ProblemDetails" /> and <see cref="ValidationProblemDetails" />.
/// </summary>
public class ErrorProblemDetailsFactory : ProblemDetailsFactory
{
    private readonly ApiBehaviorOptions _options;
    private readonly Action<ProblemDetailsContext>? _configure;

    /// <summary>
    /// Induced speial options for the problem details context.
    /// </summary>
    /// <param name="options">Dictioonary to fill the problem details with types.</param>
    /// <param name="problemDetailsOptions">If set, inside is a adjustment action for the current problem details.</param>
    public ErrorProblemDetailsFactory(
        IOptions<ApiBehaviorOptions> options,
        IOptions<ProblemDetailsOptions>? problemDetailsOptions = null)
    {
        this._options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        this._configure = problemDetailsOptions?.Value?.CustomizeProblemDetails;
    }

    /// <summary>
    /// Creates a <see cref="ProblemDetails" /> instance that configures defaults based on values specified in <see cref="ApiBehaviorOptions" />.
    /// </summary>
    /// <param name="httpContext">The <see cref="HttpContext" />.</param>
    /// <param name="statusCode">The value for <see cref="P:Microsoft.AspNetCore.Mvc.ProblemDetails.Status" />.</param>
    /// <param name="title">The value for <see cref="P:Microsoft.AspNetCore.Mvc.ProblemDetails.Title" />.</param>
    /// <param name="type">The value for <see cref="P:Microsoft.AspNetCore.Mvc.ProblemDetails.Type" />.</param>
    /// <param name="detail">The value for <see cref="P:Microsoft.AspNetCore.Mvc.ProblemDetails.Detail" />.</param>
    /// <param name="instance">The value for <see cref="P:Microsoft.AspNetCore.Mvc.ProblemDetails.Instance" />.</param>
    /// <returns>The <see cref="ProblemDetails" /> instance.</returns>
    public override ProblemDetails CreateProblemDetails(
        HttpContext httpContext,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        string? detail = null,
        string? instance = null)
    {
        statusCode ??= 500;

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Type = type,
            Detail = detail,
            Instance = instance,
        };

        this.ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

        return problemDetails;
    }

    /// <summary>
    /// Creates a <see cref="ValidationProblemDetails" /> instance that configures defaults based on values specified in <see cref="ApiBehaviorOptions" />.
    /// </summary>
    /// <param name="httpContext">The <see cref="HttpContext" />.</param>
    /// <param name="modelStateDictionary">The <see cref="ModelStateDictionary" />.</param>
    /// <param name="statusCode">The value for <see cref="P:Microsoft.AspNetCore.Mvc.ProblemDetails.Status" />.</param>
    /// <param name="title">The value for <see cref="P:Microsoft.AspNetCore.Mvc.ProblemDetails.Title" />.</param>
    /// <param name="type">The value for <see cref="P:Microsoft.AspNetCore.Mvc.ProblemDetails.Type" />.</param>
    /// <param name="detail">The value for <see cref="P:Microsoft.AspNetCore.Mvc.ProblemDetails.Detail" />.</param>
    /// <param name="instance">The value for <see cref="P:Microsoft.AspNetCore.Mvc.ProblemDetails.Instance" />.</param>
    /// <returns>The <see cref="ValidationProblemDetails" /> instance.</returns>
    public override ValidationProblemDetails CreateValidationProblemDetails(
        HttpContext httpContext,
        ModelStateDictionary modelStateDictionary,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        string? detail = null,
        string? instance = null)
    {
        if (modelStateDictionary == null)
        {
            throw new ArgumentNullException(nameof(modelStateDictionary));
        }

        statusCode ??= 400;

        var problemDetails = new ValidationProblemDetails(modelStateDictionary)
        {
            Status = statusCode,
            Type = type,
            Detail = detail,
            Instance = instance,
        };

        if (title != null)
        {
            // For validation problem details, don't overwrite the default title with null.
            problemDetails.Title = title;
        }

        this.ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

        return problemDetails;
    }

    /// <summary>
    /// Helper fiunction to fill the problem details with the right type and default title.
    /// </summary>
    /// <param name="httpContext">The <see cref="HttpContext" />.</param>
    /// <param name="problemDetails">The given problem details.</param>
    /// <param name="statusCode">The given status code to determine the right type.</param>
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

        this._configure?.Invoke(new() { HttpContext = httpContext!, ProblemDetails = problemDetails });
    }
}
