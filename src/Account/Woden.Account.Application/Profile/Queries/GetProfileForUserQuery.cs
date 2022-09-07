namespace KgNet88.Woden.Account.Application.Profile.Queries;

public class GetProfileForUserQuery : IRequest<ErrorOr<GetProfileForUserResult>>
{
    public required string Username { get; init; }
}
