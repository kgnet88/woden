namespace KgNet88.Woden.Account.Infrastructure.Auth.Persistence;

public class AuthRepository : IAuthRepository
{
    private readonly UserManager<DbUser> _userManager;

    public AuthRepository(UserManager<DbUser> userManager)
    {
        this._userManager = userManager;
    }

    public async Task<bool> DeleteUserByNameAsync(string username)
    {
        var user = await this._userManager.FindByNameAsync(username);

        if (user is null)
        {
            string message = $"A user {username} does not exist!";
            throw new ValidationException(message, new[]
            {
                new ValidationFailure(nameof(username), message)
            });
        }

        return (await this._userManager.DeleteAsync(user)).Succeeded;
    }

    public async Task<User?> GetUserByNameAsync(string username)
    {
        var dbUser = await this._userManager.FindByNameAsync(username);

        return dbUser is null
            ? null
            : new User
            {
                Id = dbUser.Id,
                Email = dbUser.Email!,
                Username = dbUser.UserName!
            };
    }

    public async Task<User> LoginUserAsync(string username, string password)
    {
        var user = await this._userManager.FindByNameAsync(username);

        if (user is null)
        {
            string message = $"A user {username} does not exist!";
            throw new ValidationException(message, new[]
            {
                new ValidationFailure(nameof(username), message)
            });
        }

        bool result = await this._userManager.CheckPasswordAsync(
            user,
            password);

        if (!result)
        {
            const string message = "username or password are wrong!";
            throw new ValidationException(message, new[]
            {
                new ValidationFailure(nameof(username), message)
            });
        }

        return new User
        {
            Id = user.Id,
            Username = user.UserName!,
            Email = user.Email!
        };
    }

    public async Task RegisterUserAsync(string username, string email, string password)
    {
        var dbUser = await this._userManager.FindByNameAsync(username);

        if (dbUser is not null)
        {
            string message = $"A user {username} already exists!";
            throw new ValidationException(message, new[]
            {
                new ValidationFailure(nameof(username), message)
            });
        }

        var newUser = new DbUser()
        {
            UserName = username,
            Email = email
        };

        var result = await this._userManager.CreateAsync(newUser, password);

        if (!result.Succeeded)
        {
            string message = $"A user with {username} cannot be registered!";
            throw new ValidationException(message, new[]
            {
                new ValidationFailure(nameof(username), message)
            });
        }
    }
}
