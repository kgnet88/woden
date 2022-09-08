namespace KgNet88.Woden.Account.Application.Profiles.Persistence;

public interface IProfileRepository
{
    public Task<ErrorOr<Created>> CreateProfileAsync(Guid UserId, UserProfile profile);
    public Task<ErrorOr<Deleted>> DeleteProfileAsync(Guid profileId);
    public Task<ErrorOr<UserProfile?>> GetProfileAsync(Guid profileId);
    public Task<ErrorOr<Success>> UpdateProfileAsync(UserProfile profile);
}
