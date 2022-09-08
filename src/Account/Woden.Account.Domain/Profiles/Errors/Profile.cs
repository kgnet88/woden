namespace KgNet88.Woden.Account.Domain.Profiles.Errors;

public static class ProfileErrors
{
    public static class Profile
    {
        public static Error UserHasProfile => Error.Conflict("Profile.User.Conflict", "the user has allready a profile.");
        public static Error DoesNotExist => Error.NotFound("Profile.NotFound", "the user does not have a profile yet.");
    }
}
