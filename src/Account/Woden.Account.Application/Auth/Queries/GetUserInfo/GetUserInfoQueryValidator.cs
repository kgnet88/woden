namespace KgNet88.Woden.Account.Application.Auth.Queries.GetUserInfo;

public class GetUserInfoQueryValidator : AbstractValidator<GetUserInfoQuery>
{
    public GetUserInfoQueryValidator()
    {
        _ = this.RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("username should not be empty.");
    }
}