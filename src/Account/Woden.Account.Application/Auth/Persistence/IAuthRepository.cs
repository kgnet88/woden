namespace KgNet88.Woden.Account.Application.Auth.Persistence;

public interface IAuthRepository
{
    public Task<ErrorOr<Success>> ChangePasswordAsync(string username, string oldPassword, string newPassword);
    public Task<ErrorOr<Success>> ChangeEmailAsync(string username, string newEmail);
    public Task<ErrorOr<Deleted>> DeleteUserByNameAsync(string username);
    public Task<ErrorOr<User>> GetUserByNameAsync(string username);
    public Task<ErrorOr<User>> LoginUserAsync(string username, string password);
    public Task<ErrorOr<Created>> RegisterUserAsync(string username, string email, string password);
}