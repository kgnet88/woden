namespace KgNet88.Woden.Account.Application.Auth.Commands.ChangeEmail;

public class ChangeEmailCommand : IRequest<ErrorOr<Success>>
{
    public required string Username { get; init; }
    public required string NewEmail { get; init; }
}
