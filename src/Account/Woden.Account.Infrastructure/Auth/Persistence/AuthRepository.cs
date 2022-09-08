namespace KgNet88.Woden.Account.Infrastructure.Auth.Persistence;

public class AuthRepository : IAuthRepository
{
    private readonly UserManager<DbUser> _userManager;

    public AuthRepository(UserManager<DbUser> userManager)
    {
        this._userManager = userManager;
    }

    public async Task<ErrorOr<Success>> ChangePasswordAsync(string username, string oldPassword, string newPassword)
    {
        var user = await this._userManager.FindByNameAsync(username);

        if (user == null)
        {
            return AuthErrors.User.DoesNotExist;
        }

        bool validCredentials = await this._userManager.CheckPasswordAsync(
            user,
            oldPassword);

        if (!validCredentials)
        {
            return AuthErrors.User.InvalidCredentials;
        }

        var result = await this._userManager.ChangePasswordAsync(user, oldPassword, newPassword);

        return result.Succeeded ? Result.Success : AuthErrors.Database.ChangePasswordFailed;
    }

    public async Task<ErrorOr<Success>> ChangeEmailAsync(string username, string newEmail)
    {
        var user = await this._userManager.FindByEmailAsync(newEmail);

        if (user is not null)
        {
            return AuthErrors.User.EmailAlreadyExists;
        }

        user = await this._userManager.FindByNameAsync(username);

        if (user is null)
        {
            return AuthErrors.User.DoesNotExist;
        }

        string token = await this._userManager.GenerateChangeEmailTokenAsync(user, newEmail);
        var result = await this._userManager.ChangeEmailAsync(user, newEmail, token);

        return result.Succeeded ? Result.Success : AuthErrors.Database.ChangeEmailFailed;
    }

    public async Task<ErrorOr<Deleted>> DeleteUserByNameAsync(string username)
    {
        var user = await this._userManager.FindByNameAsync(username);

        if (user == null)
        {
            return AuthErrors.User.DoesNotExist;
        }

        var result = await this._userManager.DeleteAsync(user);

        return result.Succeeded ? Result.Deleted : AuthErrors.Database.DeleteFailed;
    }

    public async Task<ErrorOr<User>> GetUserByNameAsync(string username)
    {
        var dbUser = await this._userManager.FindByNameAsync(username);

        return dbUser == null
            ? (ErrorOr<User>)AuthErrors.User.DoesNotExist
            : (ErrorOr<User>)new User
            {
                Username = dbUser.UserName!,
                Email = dbUser.Email!,
                Id = dbUser.Id
            };
    }

    public async Task<ErrorOr<User>> LoginUserAsync(string username, string password)
    {
        var user = await this._userManager.FindByNameAsync(username);

        if (user == null)
        {
            return AuthErrors.User.DoesNotExist;
        }

        bool result = await this._userManager.CheckPasswordAsync(
            user,
            password);

        return !result
            ? (ErrorOr<User>)AuthErrors.User.InvalidCredentials
            : (ErrorOr<User>)new User
            {
                Id = user.Id,
                Username = user.UserName!,
                Email = user.Email!
            };
    }

    public async Task<ErrorOr<Created>> RegisterUserAsync(string username, string email, string password)
    {
        if (string.IsNullOrEmpty(username))
        {
            return AuthErrors.User.UsernameEmpty;
        }

        if (string.IsNullOrEmpty(email))
        {
            return AuthErrors.User.EmailEmpty;
        }

        var dbUser = await this._userManager.FindByNameAsync(username);

        if (dbUser is not null)
        {
            return AuthErrors.User.UsernameAlreadyExists;
        }

        dbUser = await this._userManager.FindByEmailAsync(email);

        if (dbUser is not null)
        {
            return AuthErrors.User.EmailAlreadyExists;
        }

        var newUser = new DbUser()
        {
            Id = Guid.NewGuid(),
            UserName = username,
            Email = email
        };

        var result = await this._userManager.CreateAsync(newUser, password);

        return !result.Succeeded ? (ErrorOr<Created>)AuthErrors.Database.RegisterFailed : Result.Created;
    }
}
