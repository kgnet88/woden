namespace KgNet88.Woden.Account.Application.Auth.Commands.ChangePassword;

public class ChangePasswordCommand : IRequest<ErrorOr<Success>>
{
    public required string Username { get; init; }
    public required string OldPassword { get; init; }
    public required string NewPassword { get; init; }
}
