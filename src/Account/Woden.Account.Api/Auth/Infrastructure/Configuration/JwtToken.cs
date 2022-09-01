namespace Goedde88.Woden.Account.Api.Auth.Infrastructure.Configuration;

public class JwtToken
{
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public required string Secret { get; init; }
}
