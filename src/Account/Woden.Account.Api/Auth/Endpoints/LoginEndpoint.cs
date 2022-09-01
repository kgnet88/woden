namespace KgNet88.Woden.Account.Api.Auth.Endpoints;

public class LoginEndpoint : Endpoint<LoginRequest, LoginResponse>
{
    private readonly IAuthService _authService;

    public LoginEndpoint(IAuthService authService)
    {
        this._authService = authService;
    }

    public override void Configure()
    {
        this.Post("auth/login");
        this.AllowAnonymous();
    }

    public override async Task HandleAsync(LoginRequest request, CancellationToken ct)
    {
        string jwtToken = await this._authService.LoginUserAsync(request.Username, request.Password);

        var validTo = SystemClock.Instance.GetCurrentInstant() + Duration.FromMinutes(20);

        var response = new LoginResponse()
        {
            AccessToken = jwtToken,
            ValidTo = validTo
        };

        await this.SendAsync(response, 200, ct);
    }
}
