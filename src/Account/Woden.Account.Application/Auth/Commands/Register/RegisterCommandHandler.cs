using ErrorOr;

using MediatR;

namespace KgNet88.Woden.Account.Application.Auth.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<Created>>
{
    private readonly IAuthRepository _authRepository;

    public RegisterCommandHandler(IAuthRepository authRepository)
    {
        this._authRepository = authRepository;
    }

    async Task<ErrorOr<Created>> IRequestHandler<RegisterCommand, ErrorOr<Created>>.Handle(
        RegisterCommand command,
        CancellationToken cancellationToken)
    {
        return await this._authRepository.RegisterUserAsync(
    command.Username,
    command.Email,
    command.Password);
    }
}
