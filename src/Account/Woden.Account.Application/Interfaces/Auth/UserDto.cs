namespace KgNet88.Woden.Account.Application.Interfaces.Auth;

public record UserDto
{
    public required Guid Id { get; init; }
    public required string Username { get; init; }
    public required string Email { get; init; }
}
