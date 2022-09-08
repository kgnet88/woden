namespace KgNet88.Woden.Account.Contracts.Profile.Requests;

public record CreateProfileRequest
{
    public string? DisplayName { get; init; }
    public string? ProfileEmail { get; init; }
    public string? MatrixId { get; init; }
}
