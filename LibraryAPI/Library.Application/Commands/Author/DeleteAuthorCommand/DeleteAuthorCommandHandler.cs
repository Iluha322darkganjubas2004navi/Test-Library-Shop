using Library.Application.Commands.Author.DeleteAuthorCommand;
using Library.Application.Exceptions;
using Library.Infrastructure.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Commands.Author.DeleteAuthorCommand;

public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, bool>
{
    private readonly IAuthorRepository _authorRepository;

    public DeleteAuthorCommandHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<bool> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var existingAuthor = await _authorRepository.GetByIdAsync(request.Id, cancellationToken);
        if (existingAuthor == null)
        {
            throw new NotFoundException($"Author with id '{request.Id}' not found.");
        }

        await _authorRepository.DeleteAsync(existingAuthor, cancellationToken);
        return true;
    }
}