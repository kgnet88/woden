namespace KgNet88.Woden.Account.Domain.Auth.Errors;
public static partial class Errors
{
    public static class Database
    {
        public static Error RegisterFailed => Error.Unexpected(
            "Database.RegisterFailed.Unexpected",
            "The database failed to register the user.");

        public static Error DeleteFailed => Error.Unexpected(
            "Database.DeleteFailed.Unexpected",
            "The database failed to delete the user.");
    }
}