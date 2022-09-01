namespace KgNet88.Woden.Account.Api.Auth.Endpoints.Validation;

public class LoginRequestValidator : Validator<LoginRequest>
{
    public LoginRequestValidator()
    {
        _ = this.RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("username should not be empty!")
            .MinimumLength(3)
            .WithMessage("username is too short!");

        _ = this.RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("password should not be empty!")
            .MinimumLength(8)
            .WithMessage("password is too short!");
    }
}