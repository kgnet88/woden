namespace KgNet88.Woden.Account.Application.Users.Interfaces;

/// <summary>
/// Interface to abstract the json web token generation into the infrastructure layer.
/// </summary>
public interface IJwtTokenGenerator
{
    /// <summary>
    /// Generates a json web token with user related claims and given configuration.
    /// </summary>
    /// <param name="user">The user for whom the token is generated.</param>
    /// <returns>The json web token and the time point when it expires.</returns>
    public (string Token, ZonedDateTime Expires) GenerateToken(User user);
}
