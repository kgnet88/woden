namespace KgNet88.Woden.Account.Application.Auth.Commands.DeleteUserByName;

public class DeleteUserByNameCommandHandler : IRequestHandler<DeleteUserByNameCommand, ErrorOr<Deleted>>
{
    private readonly IAuthRepository _authRepository;
    private readonly IProfileRepository _profileRepository;
    private readonly IHttpContextAccessor _contextAccessor;

    public DeleteUserByNameCommandHandler(
        IAuthRepository authRepository,
        IProfileRepository profileRepository,
        IHttpContextAccessor contextAccessor)
    {
        this._authRepository = authRepository;
        this._profileRepository = profileRepository;
        this._contextAccessor = contextAccessor;
    }

    public async Task<ErrorOr<Deleted>> Handle(DeleteUserByNameCommand command, CancellationToken cancellationToken)
    {
        var identity = this._contextAccessor.HttpContext!.User;
        var userId = Guid.Parse(identity.Claims.First(x => x.Type == "sub").Value);

        var profileResult = await this._profileRepository.GetProfileAsync(userId);

        if (profileResult.IsError)
        {
            return profileResult.Errors;
        }

        if (profileResult.Value != null)
        {
            var deleteResult = await this._profileRepository.DeleteProfileAsync(profileResult.Value.Id);

            if (deleteResult.IsError)
            {
                return deleteResult;
            }
        }

        return await this._authRepository.DeleteUserByNameAsync(command.Username);
    }
}
