namespace KgNet88.Woden.Account.Application.Auth.Commands.Register;

public record RegisterCommand : IRequest<ErrorOr<Created>>
{
    public required string Username { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
}
