using ErrorOr;

using MediatR;

namespace KgNet88.Woden.Account.Application.Auth.Commands.DeleteUserByName;

public class DeleteUserByNameCommand : IRequest<ErrorOr<Deleted>>
{
    public required string Username { get; init; }
}