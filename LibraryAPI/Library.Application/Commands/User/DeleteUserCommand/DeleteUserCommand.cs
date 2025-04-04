using MediatR;

namespace Library.Application.Commands.User.DeleteUserCommand;

public sealed record DeleteUserCommand(Guid userId) : IRequest<bool>;