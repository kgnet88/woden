namespace KgNet88.Woden.Account.Application.Profiles.Commands.DeleteProfile;

public class DeleteProfileCommandHandler : IRequestHandler<DeleteProfileCommand, ErrorOr<Deleted>>
{
    private readonly IProfileRepository _profileRepository;

    public DeleteProfileCommandHandler(IProfileRepository profileRepository)
    {
        this._profileRepository = profileRepository;
    }

    public async Task<ErrorOr<Deleted>> Handle(DeleteProfileCommand command, CancellationToken cancellationToken)
    {
        return await this._profileRepository.DeleteProfileAsync(command.UserId);
    }
}
