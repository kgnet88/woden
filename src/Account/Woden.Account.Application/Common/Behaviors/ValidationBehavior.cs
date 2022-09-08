namespace KgNet88.Woden.Account.Application.Common.Behaviors;

/// <summary>
/// Validation pipeline for the MediatR request handler. It performs a request validation, if a request validator is
/// set and passes it on only if the validation is successful.
/// </summary>
/// <typeparam name="TRequest">Request for the pipeline.</typeparam>
/// <typeparam name="TResponse">Corresponding response for the pipeline.</typeparam>
public class ValidationBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IErrorOr
{
    private readonly IValidator<TRequest>? _validator;

    /// <summary>
    /// Injects the specific request validator for the pieline.
    /// </summary>
    /// <param name="validator">The specific request validator.</param>
    public ValidationBehavior(IValidator<TRequest>? validator = null)
    {
        this._validator = validator;
    }

    /// <summary>
    /// Pipeline handler. Perform any additional behavior and await the <paramref name="next" /> delegate as necessary.
    /// In this specific implementation a request validation is performed before the handler gets it.
    /// </summary>
    /// <param name="request">Incoming request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="next">Awaitable delegate for the next action in the pipeline. Eventually this delegate represents the handler.</param>
    /// <returns>Awaitable task returning the <typeparamref name="TResponse" /></returns>
    public async Task<TResponse> Handle(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        if (this._validator is null)
        {
            return await next();
        }

        var validationResult = await this._validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid)
        {
            return await next();
        }

        var errors = validationResult.Errors
            .ConvertAll(validationFailure => Error.Validation(
                validationFailure.PropertyName,
                validationFailure.ErrorMessage));

        return (dynamic)errors;
    }
}
