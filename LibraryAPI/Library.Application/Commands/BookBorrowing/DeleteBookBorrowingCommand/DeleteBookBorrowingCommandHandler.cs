using Library.Application.Commands.BookBorrowing.DeleteBookBorrowingCommand;
using Library.Application.Exceptions;
using Library.Infrastructure.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Commands.BookBorrowing.DeleteBookBorrowingCommand;

public class DeleteBookBorrowingCommandHandler : IRequestHandler<DeleteBookBorrowingCommand, bool>
{
    private readonly IBookBorrowingRepository _bookBorrowingRepository;

    public DeleteBookBorrowingCommandHandler(IBookBorrowingRepository bookBorrowingRepository)
    {
        _bookBorrowingRepository = bookBorrowingRepository;
    }

    public async Task<bool> Handle(DeleteBookBorrowingCommand request, CancellationToken cancellationToken)
    {
        var existingBorrowing = await _bookBorrowingRepository.GetByIdAsync(request.Id);
        if (existingBorrowing == null)
        {
            throw new NotFoundException($"BookBorrowing with id '{request.Id}' not found.");
        }

        await _bookBorrowingRepository.DeleteAsync(existingBorrowing);
        return true;
    }
}