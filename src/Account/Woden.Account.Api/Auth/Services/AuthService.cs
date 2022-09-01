namespace KgNet88.Woden.Account.Api.Auth.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IAuthRepository _authRepository;

    public AuthService(
        IConfiguration configuration,
        IAuthRepository authRepository)
    {
        this._configuration = configuration;
        this._authRepository = authRepository;
    }

    public async Task<bool> DeleteUserByNameAsync(string username)
    {
        return await this._authRepository.DeleteUserByNameAsync(username);
    }

    public async Task<string> LoginUserAsync(string username, string password)
    {
        var user = await this._authRepository.LoginUserAsync(username, password);

        return this.GenerateToken(user.Username);
    }

    public async Task RegisterUserAsync(User user, string password)
    {
        await this._authRepository.RegisterUserAsync(user.ToUserDto(), password);
    }

    public Task RegisterUserAsync(User user)
    {
        throw new NotImplementedException();
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
