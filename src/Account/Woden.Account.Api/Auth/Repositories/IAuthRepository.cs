namespace KgNet88.Woden.Account.Api.Auth.Repositories;

public interface IAuthRepository
{
    public Task<bool> DeleteUserByNameAsync(string username);
    public Task<UserDto> LoginUserAsync(string username, string password);
    public Task RegisterUserAsync(UserDto user, string password);
}