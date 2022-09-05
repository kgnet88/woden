namespace KgNet88.Woden.Account.Contracts.Auth.Requests;

public record LoginRequest
{
    public string Username { get; init; } = default!;
    public string Password { get; init; } = default!;
}
