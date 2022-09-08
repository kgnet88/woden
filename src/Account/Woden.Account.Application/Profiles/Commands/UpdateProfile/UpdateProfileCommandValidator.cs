namespace KgNet88.Woden.Account.Application.Profiles.Commands.UpdateProfile;

public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
{
    public UpdateProfileCommandValidator()
    {
        _ = this.RuleFor(x => x.DisplayName)
            .NotEmpty()
            .When(x => x.ProfileEmail == null && x.MatrixId == null);
        _ = this.RuleFor(x => x.ProfileEmail)
            .NotEmpty()
            .When(x => x.DisplayName == null && x.MatrixId == null);
        _ = this.RuleFor(x => x.MatrixId)
            .NotNull()
            .When(x => x.DisplayName == null && x.ProfileEmail == null);
    }
}
