namespace KgNet88.Woden.Account.Application.Auth.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        _ = this.RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("username should not be empty.");

        _ = this.RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("email should not be empty.");

        _ = this.RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("password should not be empty.");
    }
}
