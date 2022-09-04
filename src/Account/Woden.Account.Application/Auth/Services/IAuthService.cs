namespace KgNet88.Woden.Account.Application.Services;

public interface IAuthService
{
    public Task<bool> DeleteUserByNameAsync(string username);
    public Task<string> LoginUserAsync(string username, string password);
    public Task RegisterUserAsync(string username, string email, string password);
}
