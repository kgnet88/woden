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
            .WithMessage("password should not be empty.")
            .MinimumLength(8)
            .WithMessage("password must be at least 8 characters long.")
            .Must(password => password.Any(x => char.IsUpper(x)))
            .WithMessage("password must contain at least one capital letter.")
            .Must(password => password.Any(x => char.IsLower(x)))
            .WithMessage("password must contain at least one lowercase letter.")
            .Must(password => password.Any(x => char.IsDigit(x)))
            .WithMessage("password must contain at least one number.")
            .Must(password => password.Any(x => !char.IsLetterOrDigit(x)))
            .WithMessage("password must contain at least one special character.");
    }
}
