namespace KgNet88.Woden.Account.Application.Auth.Commands.ChangeEmail;
public class ChangeEmailCommandValidator : AbstractValidator<ChangeEmailCommand>
{
    public ChangeEmailCommandValidator()
    {
        _ = this.RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("username should not be empty.");

        _ = this.RuleFor(x => x.NewEmail)
            .NotEmpty()
            .WithMessage("new email should not be empty.");
    }
}