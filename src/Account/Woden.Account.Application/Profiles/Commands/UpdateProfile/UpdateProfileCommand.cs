namespace KgNet88.Woden.Account.Application.Profiles.Commands.UpdateProfile;

public class UpdateProfileCommand : IRequest<ErrorOr<Success>>
{
    public string? DisplayName { get; init; }
    public string? ProfileEmail { get; init; }
    public string? MatrixId { get; init; }
}
