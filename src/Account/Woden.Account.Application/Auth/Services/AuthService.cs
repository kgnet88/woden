namespace KgNet88.Woden.Account.Application.Auth.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(
        IAuthRepository authRepository,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        this._authRepository = authRepository;
        this._jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<bool> DeleteUserByNameAsync(string username)
    {
        return await this._authRepository.DeleteUserByNameAsync(username);
    }

    public async Task<string> LoginUserAsync(string username, string password)
    {
        var user = await this._authRepository.LoginUserAsync(username, password);

        return this._jwtTokenGenerator.GenerateToken(user.Id, user.Username, user.Email);
    }

    public async Task RegisterUserAsync(string username, string email, string password)
    {
        await this._authRepository.RegisterUserAsync(username, email, password);
    }
}
