namespace KgNet88.Woden.Account.Infrastructure.Profiles.Persistence.Database.Model;

public record DbUserProfile
{
    public required Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public required string DisplayName { get; set; }
    public required string ProfileEmail { get; set; }
    public string MatrixId { get; set; } = default!;
}
