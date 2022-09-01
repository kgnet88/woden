namespace Goedde88.Woden.Account.Api.Auth.Endpoints.Validation;

public class RegisterRequestValidator : Validator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        _ = this.RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("username should not be empty!")
            .MinimumLength(3)
            .WithMessage("username is too short!");

        _ = this.RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("email should not be empty!")
            .EmailAddress()
            .WithMessage("email is invalid!");

        _ = this.RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("password should not be empty!")
            .MinimumLength(8)
            .WithMessage("password is too short!");
    }
}