using KgNet88.Woden.Account.Application.Services;

namespace KgNet88.Woden.Account.Api.Auth.Endpoints;

public class DeleteUserByNameEndpoint : EndpointWithoutRequest
{
    private readonly IAuthService _authService;

    public DeleteUserByNameEndpoint(IAuthService authService)
    {
        this._authService = authService;
    }

    public override void Configure()
    {
        this.Delete("auth/user/{Username}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        string? username = this.Route<string>("Username");

        if (username is null)
        {
            this.ThrowError("missing username!");
        }

        bool isDeleted = await this._authService.DeleteUserByNameAsync(username);

        if (!isDeleted)
        {
            this.ThrowError("user could not be deleted!");
        }

        await this.SendOkAsync(ct);
    }
}