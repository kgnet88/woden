using KgNet88.Woden.Account.Application.Services;

namespace KgNet88.Woden.Account.Api.Auth.Endpoints;

public class RegisterEndpoint : Endpoint<RegisterRequest>
{
    private readonly IAuthService _authService;

    public RegisterEndpoint(IAuthService authService)
    {
        this._authService = authService;
    }

    public override void Configure()
    {
        this.Post("auth/register");
        this.AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterRequest request, CancellationToken ct)
    {
        await this._authService.RegisterUserAsync(request.Username, request.Email, request.Password);

        this.ThrowIfAnyErrors();

        await this.SendOkAsync(ct);
    }
}
