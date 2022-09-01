namespace KgNet88.Woden.Account.Api.Auth.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly UserManager<DbUser> _userManager;
    private readonly SignInManager<DbUser> _signInManager;

    public AuthRepository(UserManager<DbUser> userManager, SignInManager<DbUser> signInManager)
    {
        this._userManager = userManager;
        this._signInManager = signInManager;
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

    public async Task<UserDto> LoginUserAsync(string username, string password)
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

        var result = await this._signInManager.CheckPasswordSignInAsync(
            user,
            password,
            false);

        if (!result.Succeeded)
        {
            const string message = "username or password are wrong!";
            throw new ValidationException(message, new[]
            {
                new ValidationFailure(nameof(username), message)
            });
        }

        return new UserDto
        {
            Username = user.UserName!,
            Email = user.Email!
        };
    }

    public async Task RegisterUserAsync(UserDto user, string password)
    {
        var dbUser = await this._userManager.FindByNameAsync(user.Username);

        if (dbUser is not null)
        {
            string message = $"A user {user.Username} already exists!";
            throw new ValidationException(message, new[]
            {
                new ValidationFailure(nameof(user), message)
            });
        }

        var newUser = new DbUser()
        {
            UserName = user.Username,
            Email = user.Email
        };

        var result = await this._userManager.CreateAsync(newUser, password);

        if (!result.Succeeded)
        {
            string message = $"A user with {user.Username} cannot be registered!";
            throw new ValidationException(message, new[]
            {
                new ValidationFailure(nameof(user), message)
            });
        }
    }
}
