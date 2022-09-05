namespace KgNet88.Woden.Account.Api.Auth.Endpoints;

public class DeleteUserByNameEndpoint : EndpointWithoutRequest
{
    private readonly ISender _mediator;
    private readonly ProblemDetailsFactory _problemDetailsFactory;

    public DeleteUserByNameEndpoint(ISender mediator, ProblemDetailsFactory problemDetailsFactory)
    {
        this._mediator = mediator;
        this._problemDetailsFactory = problemDetailsFactory;
    }

    public override void Configure()
    {
        this.Delete("auth/user/{Username}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        string username = this.Route<string>("Username") ?? "";

        var command = new DeleteUserByNameCommand()
        {
            Username = username,
        };

        var result = await this._mediator.Send(command, ct);

        if (result.IsError)
        {
            await result.SendProblemDetailsAsync(this.HttpContext, this._problemDetailsFactory);
            return;
        }

        await this.SendOkAsync(ct);
    }
}