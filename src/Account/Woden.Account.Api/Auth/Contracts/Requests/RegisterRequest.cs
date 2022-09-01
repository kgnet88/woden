namespace KgNet88.Woden.Account.Api.Auth.Contracts.Requests;

public class LoginRequest
{
    public string Username { get; init; } = default!;
    public string Password { get; init; } = default!;
}
