namespace KgNet88.Woden.Account.Contracts.Auth.Requests;

public record ChangePasswordRequest
{
    public string Username { get; init; } = default!;
    public string OldPassword { get; init; } = default!;
    public string NewPassword { get; init; } = default!;
}