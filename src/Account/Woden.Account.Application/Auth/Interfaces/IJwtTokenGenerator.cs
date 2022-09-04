namespace KgNet88.Woden.Account.Application.Auth.Interfaces;
public interface IJwtTokenGenerator
{
    public string GenerateToken(User user);
}
