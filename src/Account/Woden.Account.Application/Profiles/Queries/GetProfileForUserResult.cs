namespace KgNet88.Woden.Account.Application.Profiles.Queries;

public class GetProfileForUserResult
{
    public required string DisplayName { get; init; }
    public required string ProfileEmail { get; init; }
    public string MatrixId { get; init; } = default!;
}
