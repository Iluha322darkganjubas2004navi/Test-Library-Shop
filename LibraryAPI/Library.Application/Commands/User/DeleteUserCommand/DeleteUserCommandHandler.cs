using Library.Application.Exceptions;
using MediatR;
using Library.Application.Exceptions;
using Library.Infrastructure.Repositories;

namespace Library.Application.Commands.User.DeleteUserCommand;

public class DeleteUserCommandHandler(IUserRepository userRepository) : IRequestHandler<DeleteUserCommand, bool>
{
    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await userRepository.GetByIdAsync(request.userId);

        if (existingUser == null)
        {
            throw new NotFoundException("User not found");
        }

        await userRepository.DeleteAsync(existingUser);

        return true;
    }
}