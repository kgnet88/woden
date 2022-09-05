namespace KgNet88.Woden.Account.Application.Auth.Queries.Login;

public class LoginQuery : IRequest<ErrorOr<LoginResult>>
{
    public required string Username { get; init; }
    public required string Password { get; init; }
}