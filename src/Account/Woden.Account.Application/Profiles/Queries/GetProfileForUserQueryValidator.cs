namespace KgNet88.Woden.Account.Application.Profiles.Queries;

public class GetProfileForUserQueryValidator : AbstractValidator<GetProfileForUserQuery>
{
    public GetProfileForUserQueryValidator()
    {
        _ = this.RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("username should not be empty.");
    }
}