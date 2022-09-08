namespace KgNet88.Woden.Account.Application.Profiles.Commands.CreateProfile;

public class CreateProfileCommandHandler : IRequestHandler<DeleteProfileCommand, ErrorOr<Created>>
{
    private readonly IProfileRepository _profileRepository;
    private readonly IAuthRepository _authRepository;
    private readonly IHttpContextAccessor _contextAccessor;

    public CreateProfileCommandHandler(
        IProfileRepository profileRepository,
        IAuthRepository authRepository,
        IHttpContextAccessor contextAccessor)
    {
        this._profileRepository = profileRepository;
        this._authRepository = authRepository;
        this._contextAccessor = contextAccessor;
    }

    public async Task<ErrorOr<Created>> Handle(DeleteProfileCommand request, CancellationToken cancellationToken)
    {
        var identity = this._contextAccessor.HttpContext!.User;
        string username = identity.Claims.First(x => x.Type == "Username").Value;

        var userResult = await this._authRepository.GetUserByNameAsync(username);

        if (userResult.IsError)
        {
            return userResult.Errors;
        }

        var profile = new UserProfile
        {
            Id = Guid.NewGuid(),
            DisplayName = request.DisplayName ?? userResult.Value.Username,
            ProfileEmail = request.ProfileEmail ?? userResult.Value.Email,
            MatrixId = request.MatrixId ?? string.Empty
        };

        return await this._profileRepository.CreateProfileAsync(userResult.Value.Id, profile);
    }
}
