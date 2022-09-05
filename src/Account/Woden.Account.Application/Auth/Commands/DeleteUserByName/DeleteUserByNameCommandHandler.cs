using ErrorOr;

using MediatR;

namespace KgNet88.Woden.Account.Application.Auth.Commands.DeleteUserByName;

public class DeleteUserByNameCommandHandler : IRequestHandler<DeleteUserByNameCommand, ErrorOr<Deleted>>
{
    private readonly IAuthRepository _authRepository;

    public DeleteUserByNameCommandHandler(IAuthRepository authRepository)
    {
        this._authRepository = authRepository;
    }

    public async Task<ErrorOr<Deleted>> Handle(DeleteUserByNameCommand request, CancellationToken cancellationToken)
    {
        return await this._authRepository.DeleteUserByNameAsync(request.Username);
    }
}
