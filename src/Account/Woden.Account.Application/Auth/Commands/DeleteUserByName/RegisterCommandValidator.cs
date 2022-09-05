using FluentValidation;

namespace KgNet88.Woden.Account.Application.Auth.Commands.DeleteUserByName;

public class DeleteUserByNameCommandValidator : AbstractValidator<DeleteUserByNameCommand>
{
    public DeleteUserByNameCommandValidator()
    {
        _ = this.RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("username should not be empty.");
    }
}
