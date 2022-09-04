namespace KgNet88.Woden.Account.Application.Auth.Persistence;

public interface IAuthRepository
{
    public Task<bool> DeleteUserByNameAsync(string username);
    public Task<User?> GetUserByNameAsync(string username);
    public Task<User> LoginUserAsync(string username, string password);
    public Task RegisterUserAsync(string username, string email, string password);
}