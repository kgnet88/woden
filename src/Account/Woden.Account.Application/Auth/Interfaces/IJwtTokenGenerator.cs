namespace KgNet88.Woden.Account.Application.Auth.Interfaces;
public interface IJwtTokenGenerator
{
    public (string Token, ZonedDateTime Expires) GenerateToken(User user);
}
