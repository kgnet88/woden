namespace KgNet88.Woden.Account.Contracts.Auth.Requests;

public record RegisterRequest
{
    public string Username { get; init; } = default!;
    public string Email { get; init; } = default!;
    public string Password { get; init; } = default!;
}