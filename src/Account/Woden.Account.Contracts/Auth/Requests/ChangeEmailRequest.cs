namespace KgNet88.Woden.Account.Contracts.Auth.Requests;

public record ChangeEmailRequest
{
    public string Username { get; init; } = default!;
    public string NewEmail { get; init; } = default!;
}
