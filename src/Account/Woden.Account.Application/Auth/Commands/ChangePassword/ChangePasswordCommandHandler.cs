namespace KgNet88.Woden.Account.Application.Auth.Commands.ChangePassword;
public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ErrorOr<Success>>
{
    private readonly IAuthRepository _authRepository;

    public ChangePasswordCommandHandler(IAuthRepository authRepository)
    {
        this._authRepository = authRepository;
    }

    async Task<ErrorOr<Success>> IRequestHandler<ChangePasswordCommand, ErrorOr<Success>>.Handle(
        ChangePasswordCommand command,
        CancellationToken cancellationToken)
    {
        return await this._authRepository.ChangePasswordAsync(
    command.Username,
    command.OldPassword,
    command.NewPassword);
    }
}