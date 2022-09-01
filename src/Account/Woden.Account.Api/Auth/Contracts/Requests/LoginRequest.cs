namespace KgNet88.Woden.Account.Api.Auth.Contracts.Requests;

public class RegisterRequest
{
    public string Username { get; init; } = default!;
    public string Email { get; init; } = default!;
    public string Password { get; init; } = default!;
}
