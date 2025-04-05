using Library.Application.Commands.Book.DeleteBookCommand;
using Library.Application.Exceptions;
using Library.Infrastructure.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Commands.Book.DeleteBookCommand;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, bool>
{
    private readonly IBookRepository _bookRepository;

    public DeleteBookCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var existingBook = await _bookRepository.GetByIdAsync(request.Id);
        if (existingBook == null)
        {
            throw new NotFoundException($"Book with id '{request.Id}' not found.");
        }

        await _bookRepository.DeleteAsync(existingBook);
        return true;
    }
}