namespace KgNet88.Woden.Account.Application.Profile.Commands.CreateProfile;

public class DeleteProfileCommand : IRequest<ErrorOr<Created>>
{
    public string? DisplayName { get; init; }
    public string? ProfileEmail { get; init; }
    public string? MatrixId { get; init; }
}
