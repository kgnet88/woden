namespace KgNet88.Woden.Account.Infrastructure.Users.Implementations;

/// <summary>
/// Implementation for the generation of json web tokens on given infrastructure.
/// </summary>
public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;
    private readonly IDateTimeProvider _dateTimeProvider;

    /// <summary>
    /// Injects necessary services to generate a valid user token.
    /// </summary>
    /// <param name="jwtOptions">The neccessary jwt configuration.</param>
    /// <param name="dateTimeProvider">The date time provider to calculate expiration time.</param>
    public JwtTokenGenerator(IOptions<JwtSettings> jwtOptions, IDateTimeProvider dateTimeProvider)
    {
        this._jwtSettings = jwtOptions.Value;
        this._dateTimeProvider = dateTimeProvider;
    }

    /// <summary>
    /// Generates a json web token with user related claims and given configuration.
    /// </summary>
    /// <param name="user">The user for whom the token is generated.</param>
    /// <returns>The json web token and the time point when it expires.</returns>
    public (string Token, ZonedDateTime Expires) GenerateToken(User user)
    {
        var expires = this._dateTimeProvider.UtcNow + Duration.FromMinutes(this._jwtSettings.ExpiryMinutes);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Credentials.Email),
            new Claim("Username", user.Credentials.Username)
        };

        string token = JWTBearer.CreateToken(
               signingKey: this._jwtSettings.Secret,
               issuer: this._jwtSettings.Issuer,
               audience: this._jwtSettings.Audience,
               expireAt: expires.ToDateTimeUtc(),
               claims: claims);

        return (token, expires);
    }
}
