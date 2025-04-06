using AutoMapper;
using Library.Application.DTOs;
using Library.Application.Exceptions;
using Library.Domain.DTOs;
using Library.Domain.Entities;
using Library.Infrastructure.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Queries.BookBorrowing.GetBookBorrowingByIdQuery;

public class GetBookBorrowingByIdQueryHandler : IRequestHandler<GetBookBorrowingByIdQuery, BookBorrowingDTO>
{
    private readonly IBookBorrowingRepository _bookBorrowingRepository;
    private readonly IMapper _mapper;

    public GetBookBorrowingByIdQueryHandler(IBookBorrowingRepository bookBorrowingRepository, IMapper mapper)
    {
        _bookBorrowingRepository = bookBorrowingRepository;
        _mapper = mapper;
    }

    public async Task<BookBorrowingDTO> Handle(GetBookBorrowingByIdQuery request, CancellationToken cancellationToken)
    {
        var bookBorrowing = await _bookBorrowingRepository.GetByIdAsync(request.BorrowingId);

        if (bookBorrowing == null)
        {
            throw new NotFoundException($"Book borrowing with id '{request.BorrowingId}' not found.");
        }

        return _mapper.Map<BookBorrowingDTO>(bookBorrowing);
    }
}