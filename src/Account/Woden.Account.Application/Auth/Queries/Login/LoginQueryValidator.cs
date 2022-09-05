namespace KgNet88.Woden.Account.Application.Auth.Queries.Login;

public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        _ = this.RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("username should not be empty.");

        _ = this.RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("password should not be empty.");
    }
}
