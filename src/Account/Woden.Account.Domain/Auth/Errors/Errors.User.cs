namespace KgNet88.Woden.Account.Domain.Auth.Errors;
public static partial class Errors
{
    public static class User
    {
        // User
        public static Error DoesNotExist => Error.NotFound("User.NotFound", "user does not exist.");
        public static Error InvalidCredentials => Error.Failure("User.InvalidCredentials.Failure", "the user credentials are invalid.");

        // Username
        public static Error UsernameAlreadyExists => Error.Conflict(code: "Username.Conflict", description: "a user with that username exists already.");
        public static Error UsernameEmpty => Error.Validation(code: "Username.Empty.Validation", description: "username is invalid.");
        public static Error UsernameTooShort => Error.Validation(code: "Username.TooShort.Validation", description: "username is invalid.");
        public static Error UsernameTooLong => Error.Validation(code: "Username.TooLong.Validation", description: "username is invalid.");
        public static Error UsernameBroken => Error.Validation(code: "Username.Broken.Validation", description: "username is invalid.");

        // Email
        public static Error EmailAlreadyExists => Error.Conflict(code: "Email.Conflict", description: "a user with that email exists already.");
        public static Error EmailEmpty => Error.Validation(code: "Email.Empty.Validation", description: "email is invalid.");
        public static Error EmailRegex => Error.Validation(code: "Email.Regex.Validation", description: "email is invalid.");
    }
}
