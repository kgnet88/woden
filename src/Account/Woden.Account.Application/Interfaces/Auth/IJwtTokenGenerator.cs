namespace KgNet88.Woden.Account.Application.Interfaces.Auth;
public interface IJwtTokenGenerator
{
    public string GenerateToken(Guid userId, string username, string email);
}
