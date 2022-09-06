namespace KgNet88.Woden.Account.Application.Auth.Queries.GetUserInfo;

public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, ErrorOr<GetUserInfoResult>>
{
    private readonly IAuthRepository _authRepository;

    public GetUserInfoQueryHandler(IAuthRepository authRepository)
    {
        this._authRepository = authRepository;
    }

    public async Task<ErrorOr<GetUserInfoResult>> Handle(GetUserInfoQuery query, CancellationToken cancellationToken)
    {
        var user = await this._authRepository.GetUserByNameAsync(query.Username);

        return user.IsError
            ? (ErrorOr<GetUserInfoResult>)user.Errors
            : new GetUserInfoResult
            {
                Username = user.Value.Username,
                Email = user.Value.Email
            };
    }
}