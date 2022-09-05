using ErrorOr;

using MediatR;

namespace KgNet88.Woden.Account.Application.Auth.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<LoginResult>>
{
    private readonly IAuthRepository _authRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginQueryHandler(
        IAuthRepository authRepository,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        this._authRepository = authRepository;
        this._jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<LoginResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await this._authRepository.LoginUserAsync(request.Username, request.Password);

        if (user.IsError)
        {
            return user.Errors;
        }

        var (Token, Expires) = this._jwtTokenGenerator.GenerateToken(user.Value);

        return new LoginResult
        {
            Token = Token,
            ValidTo = Expires
        };
    }
}