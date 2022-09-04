namespace KgNet88.Woden.Account.Infrastructure.Auth.Implementations;
public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;
    private readonly IDateTimeProvider _dateTimeProvider;

    public JwtTokenGenerator(IOptions<JwtSettings> jwtOptions, IDateTimeProvider dateTimeProvider)
    {
        this._jwtSettings = jwtOptions.Value;
        this._dateTimeProvider = dateTimeProvider;
    }

    public string GenerateToken(User user)
    {
        var expires = this._dateTimeProvider.UtcNow + Duration.FromMinutes(this._jwtSettings.ExpiryMinutes);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("Username", user.Username)
        };

        return JWTBearer.CreateToken(
               signingKey: this._jwtSettings.Secret,
               issuer: this._jwtSettings.Issuer,
               audience: this._jwtSettings.Audience,
               expireAt: expires.ToDateTimeUtc(),
               claims: claims);
    }
}
