namespace KgNet88.Woden.Account.Infrastructure.Profiles.Persistence;

public class ProfileRepository : IProfileRepository
{
    private readonly ProfileDbContext _profileDbContext;

    public ProfileRepository(ProfileDbContext profileDbContext)
    {
        this._profileDbContext = profileDbContext;
    }

    public async Task<ErrorOr<Created>> CreateProfileAsync(Guid userId, UserProfile profile)
    {
        if (this._profileDbContext.Profiles.Any(x => x.UserId == userId))
        {
            return ProfileErrors.Profile.UserHasProfile;
        }

        _ = await this._profileDbContext.Profiles.AddAsync(new DbUserProfile
        {
            Id = profile.Id,
            UserId = userId,
            DisplayName = profile.DisplayName,
            MatrixId = profile.MatrixId,
            ProfileEmail = profile.ProfileEmail
        });

        _ = await this._profileDbContext.SaveChangesAsync();

        return Result.Created;
    }

    public async Task<ErrorOr<Deleted>> DeleteProfileAsync(Guid profileId)
    {
        var profile = await this._profileDbContext.Profiles.FirstOrDefaultAsync(x => x.Id == profileId);

        if (profile is null)
        {
            return ProfileErrors.Profile.DoesNotExist;
        }

        _ = this._profileDbContext.Profiles.Remove(profile!);
        _ = await this._profileDbContext.SaveChangesAsync();

        return Result.Deleted;
    }

    public async Task<ErrorOr<UserProfile?>> GetProfileAsync(Guid profileId)
    {
        var profile = await this._profileDbContext.Profiles.FirstOrDefaultAsync(x => x.Id == profileId);

        return profile is null
            ? default
            : new UserProfile
            {
                DisplayName = profile.DisplayName,
                Id = profile.Id,
                MatrixId = profile.MatrixId,
                ProfileEmail = profile.ProfileEmail
            };
    }

    public async Task<ErrorOr<Success>> UpdateProfileAsync(UserProfile profile)
    {
        var oldProfile = await this._profileDbContext.Profiles.FirstOrDefaultAsync(x => x.Id == profile.Id);

        if (oldProfile is null)
        {
            return ProfileErrors.Profile.DoesNotExist;
        }

        oldProfile.DisplayName = profile.DisplayName ?? oldProfile.DisplayName;
        oldProfile.MatrixId = profile.MatrixId ?? oldProfile.MatrixId;
        oldProfile.ProfileEmail = profile.ProfileEmail ?? oldProfile.ProfileEmail;

        return Result.Success;
    }
}
