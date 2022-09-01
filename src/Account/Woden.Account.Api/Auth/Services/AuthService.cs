namespace Goedde88.Woden.Account.Api.Auth.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<DbUser> _userManager;
    private readonly SignInManager<DbUser> _signInManager;

    public AuthService(
        IConfiguration configuration,
        UserManager<DbUser> userManager,
        SignInManager<DbUser> signInManager)
    {
        this._configuration = configuration;
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

    public async Task<string> LoginUserAsync(string username, string password)
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

        return this.GenerateToken(username);
    }

    public async Task RegisterUserAsync(User user)
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

        var result = await this._userManager.CreateAsync(newUser, user.Password);

        if (!result.Succeeded)
        {
            string message = $"A user with {user.Username} cannot be registered!";
            throw new ValidationException(message, new[]
            {
                new ValidationFailure(nameof(user), message)
            });
        }
    }

    private string GenerateToken(string username)
    {
        var validTo = SystemClock.Instance.GetCurrentInstant() + Duration.FromMinutes(20);

        var tokenConfig = this.GetTokenConfig();

        return JWTBearer.CreateToken(
               signingKey: tokenConfig.Secret,
               issuer: tokenConfig.Issuer,
               audience: tokenConfig.Audience,
               expireAt: validTo.ToDateTimeUtc(),
               claims: new (string claimType, string claimValue)[] { ("Username", username) });
    }

    private JwtToken GetTokenConfig()
    {
        if (this._configuration != null)
        {
            var jwtConfig = this._configuration.GetSection("JwtToken").Get<JwtToken>();

            if (jwtConfig != null)
            {
                return jwtConfig;
            }
        }

        return new JwtToken()
        {
            Issuer = "localhost",
            Audience = "localhost",
            Secret = "TokenSigningKeyAVeryDarkSecretString"
        };
    }
}
