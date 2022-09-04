namespace KgNet88.Woden.Account.Application.Auth.Interfaces;
public interface IJwtTokenGenerator
{
    public string GenerateToken(Guid userId, string username, string email);
}
