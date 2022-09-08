namespace KgNet88.Woden.Account.Api.User.Endpoints;

/// <summary>
/// Endpoint for register requests.
/// </summary>
public class RegisterEndpoint : Endpoint<RegisterRequest, ErrorOr<Created>>
{
    /// <summary>
    /// use this method to configure how the endpoint should be listening to incoming requests.
    /// <para>HINT: it is only called once during endpoint auto registration during app startup.</para>
    /// </summary>
    public override void Configure()
    {
        this.Post("auth/register");
        this.AllowAnonymous();
    }

    /// <summary>
    /// the handler method for the endpoint. this method is called for each request received.
    /// </summary>
    /// <param name="request">the request dto</param>
    /// <param name="ct">a cancellation token</param>
    public override async Task HandleAsync(RegisterRequest request, CancellationToken ct)
    {
        await this.SendOkAsync(ct);
    }
}