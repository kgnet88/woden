namespace KgNet88.Woden.Account.Application.Profiles.Queries;

public class GetProfileForUserQueryHandler : IRequestHandler<GetProfileForUserQuery, ErrorOr<GetProfileForUserResult>>
{
    private readonly IProfileRepository _profileRepository;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetProfileForUserQueryHandler(
        IProfileRepository profileRepository,
        IHttpContextAccessor contextAccessor)
    {
        this._profileRepository = profileRepository;
        this._contextAccessor = contextAccessor;
    }

    public async Task<ErrorOr<GetProfileForUserResult>> Handle(GetProfileForUserQuery query, CancellationToken cancellationToken)
    {
        var identity = this._contextAccessor.HttpContext!.User;
        var userId = Guid.Parse(identity.Claims.First(x => x.Type == "sub").Value);

        var result = await this._profileRepository.GetProfileAsync(userId);

        return result.IsError
            ? result.Errors
            : new GetProfileForUserResult
            {
                DisplayName = result.Value!.DisplayName,
                ProfileEmail = result.Value!.ProfileEmail,
                MatrixId = result.Value!.MatrixId,
            };
    }
}
