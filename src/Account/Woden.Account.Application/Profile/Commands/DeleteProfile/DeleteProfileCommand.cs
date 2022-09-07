namespace KgNet88.Woden.Account.Application.Profile.Commands.DeleteProfile;

public class DeleteProfileCommand : IRequest<ErrorOr<Deleted>>
{
    public Guid UserId { get; set; }
}
