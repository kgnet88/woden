namespace KgNet88.Woden.Account.Application.Auth.Queries.Login;

public class LoginResult
{
    public required string Token { get; init; }
    public required ZonedDateTime ValidTo { get; init; }
}