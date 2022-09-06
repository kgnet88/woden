namespace KgNet88.Woden.Account.Application.Auth.Commands.ChangePassword;
public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        _ = this.RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("username should not be empty.");

        _ = this.RuleFor(x => x.OldPassword)
            .NotEmpty()
            .WithMessage("old password should not be empty.");

        _ = this.RuleFor(x => x.NewPassword)
            .NotEmpty()
            .WithMessage("new password should not be empty.")
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