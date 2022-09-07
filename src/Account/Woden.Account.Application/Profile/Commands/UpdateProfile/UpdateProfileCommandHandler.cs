namespace KgNet88.Woden.Account.Application.Profile.Commands.UpdateProfile;
public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, ErrorOr<Success>>
{
    private readonly IProfileRepository _profileRepository;
    private readonly IHttpContextAccessor _contextAccessor;

    public UpdateProfileCommandHandler(
        IProfileRepository profileRepository,
        IHttpContextAccessor contextAccessor)
    {
        this._profileRepository = profileRepository;
        this._contextAccessor = contextAccessor;
    }

    public async Task<ErrorOr<Success>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var identity = this._contextAccessor.HttpContext!.User;
        var userId = Guid.Parse(identity.Claims.First(x => x.Type == "sub").Value);

        var profileResult = await this._profileRepository.GetProfileAsync(userId);

        if (profileResult.IsError)
        {
            return profileResult.Errors;
        }

        if (profileResult.Value is null)
        {
            throw new Exception("TODO");
        }

        var profile = new UserProfile
        {
            Id = profileResult.Value.Id,
            DisplayName = request.DisplayName ?? profileResult.Value.DisplayName,
            ProfileEmail = request.ProfileEmail ?? profileResult.Value.ProfileEmail,
            MatrixId = request.MatrixId ?? profileResult.Value.MatrixId
        };

        return await this._profileRepository.UpdateProfileAsync(profile);
    }
}
