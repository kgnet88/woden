namespace KgNet88.Woden.Account.Infrastructure.Profile.Persistence.Database.Model;

public record DbUserProfile
{
    public required Guid Id { get; init; }
    public required Guid UserId { get; init; }
    public required string DisplayName { get; init; }
    public required string ProfileEmail { get; init; }
    public string MatrixId { get; init; } = default!;
}
