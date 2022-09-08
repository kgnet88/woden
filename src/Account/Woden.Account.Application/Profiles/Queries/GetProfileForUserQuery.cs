namespace KgNet88.Woden.Account.Application.Profiles.Queries;

public class GetProfileForUserQuery : IRequest<ErrorOr<GetProfileForUserResult>>
{
    public required string Username { get; init; }
}
