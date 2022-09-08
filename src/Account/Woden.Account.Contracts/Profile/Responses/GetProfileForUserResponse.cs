namespace KgNet88.Woden.Account.Contracts.Profile.Responses;

public class GetProfileForUserResponse
{
    public required string DisplayName { get; init; }
    public required string ProfileEmail { get; init; }
    public string MatrixId { get; init; } = default!;
}
