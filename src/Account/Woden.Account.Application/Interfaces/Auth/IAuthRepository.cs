namespace KgNet88.Woden.Account.Application.Interfaces.Auth;

public interface IAuthRepository
{
    public Task<bool> DeleteUserByNameAsync(string username);
    public Task<bool> GetUserByNameAsync(string username);
    public Task<UserDto> LoginUserAsync(string username, string password);
    public Task RegisterUserAsync(string username, string email, string password);
}