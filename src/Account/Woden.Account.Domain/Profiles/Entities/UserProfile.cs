namespace KgNet88.Woden.Account.Domain.Profiles.Entities;

public record UserProfile
{
    public required Guid Id { get; init; }
    public required string DisplayName { get; init; }
    public required string ProfileEmail { get; init; }
    public string MatrixId { get; init; } = default!;
}
