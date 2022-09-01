namespace KgNet88.Woden.Account.Api.Auth.Services;

public interface IAuthService
{
    public Task<bool> DeleteUserByNameAsync(string username);
    public Task<string> LoginUserAsync(string username, string password);
    public Task RegisterUserAsync(User user, string password);
}
