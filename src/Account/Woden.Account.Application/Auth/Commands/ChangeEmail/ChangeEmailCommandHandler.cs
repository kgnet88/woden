namespace KgNet88.Woden.Account.Application.Auth.Commands.ChangeEmail;
public class ChangeEmailCommandHandler : IRequestHandler<ChangeEmailCommand, ErrorOr<Success>>
{
    private readonly IAuthRepository _authRepository;

    public ChangeEmailCommandHandler(IAuthRepository authRepository)
    {
        this._authRepository = authRepository;
    }

    async Task<ErrorOr<Success>> IRequestHandler<ChangeEmailCommand, ErrorOr<Success>>.Handle(
        ChangeEmailCommand command,
        CancellationToken cancellationToken)
    {
        return await this._authRepository.ChangeEmailAsync(
    command.Username,
    command.NewEmail);
    }
}