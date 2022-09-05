namespace KgNet88.Woden.Account.Infrastructure.Auth.Persistence;

public class AuthRepository : IAuthRepository
{
    private readonly UserManager<DbUser> _userManager;

    public AuthRepository(UserManager<DbUser> userManager)
    {
        this._userManager = userManager;
    }

    public async Task<ErrorOr<Deleted>> DeleteUserByNameAsync(string username)
    {
        var user = await this._userManager.FindByNameAsync(username);

        if (user == null)
        {
            return Errors.User.DoesNotExist;
        }

        var result = await this._userManager.DeleteAsync(user);

        return result.Succeeded ? Result.Deleted : Errors.Database.DeleteFailed;
    }

    public async Task<ErrorOr<User>> GetUserByNameAsync(string username)
    {
        var dbUser = await this._userManager.FindByNameAsync(username);

        return dbUser == null
            ? (ErrorOr<User>)Errors.User.DoesNotExist
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
            return Errors.User.DoesNotExist;
        }

        bool result = await this._userManager.CheckPasswordAsync(
            user,
            password);

        return !result
            ? (ErrorOr<User>)Errors.User.InvalidCredentials
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
            return Errors.User.UsernameEmpty;
        }

        if (string.IsNullOrEmpty(email))
        {
            return Errors.User.EmailEmpty;
        }

        var dbUser = await this._userManager.FindByNameAsync(username);

        if (dbUser is not null)
        {
            return Errors.User.UsernameAlreadyExists;
        }

        dbUser = await this._userManager.FindByEmailAsync(email);

        if (dbUser is not null)
        {
            return Errors.User.EmailAlreadyExists;
        }

        var newUser = new DbUser()
        {
            Id = Guid.NewGuid(),
            UserName = username,
            Email = email
        };

        var result = await this._userManager.CreateAsync(newUser, password);

        return !result.Succeeded ? (ErrorOr<Created>)Errors.Database.RegisterFailed : Result.Created;
    }
}
