namespace KgNet88.Woden.Account.Application.Auth.Queries.GetUserInfo;

public class GetUserInfoQuery : IRequest<ErrorOr<GetUserInfoResult>>
{
    public required string Username { get; init; }
}