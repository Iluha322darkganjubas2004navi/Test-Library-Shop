using AutoMapper;
using FluentValidation;
using Library.Application.Exceptions;
using Library.Application.Queries.BookBorrowing.GetAllBookBorrowingsByBookIdQuery;
using Library.Domain.DTOs;
using Library.Infrastructure.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Queries.BookBorrowing.GetAllBookBorrowingsByBookIdQuery;

public class GetAllBookBorrowingsByBookIdQueryHandler : IRequestHandler<GetAllBookBorrowingsByBookIdQuery, IEnumerable<BookBorrowingDTO>>
{
    private readonly IBookBorrowingRepository _bookBorrowingRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<GetAllBookBorrowingsByBookIdQuery> _validator;

    public GetAllBookBorrowingsByBookIdQueryHandler(IBookBorrowingRepository bookBorrowingRepository,
                                                     IBookRepository bookRepository,
                                                     IMapper mapper,
                                                     IValidator<GetAllBookBorrowingsByBookIdQuery> validator)
    {
        _bookBorrowingRepository = bookBorrowingRepository;
        _bookRepository = bookRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<IEnumerable<BookBorrowingDTO>> Handle(GetAllBookBorrowingsByBookIdQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var existingBook = await _bookRepository.GetByIdAsync(request.BookId, cancellationToken);
        if (existingBook == null)
        {
            throw new NotFoundException($"Book with id '{request.BookId}' not found.");
        }

        var borrowings = await _bookBorrowingRepository.GetBookBorrowingsByBookIdAsync(request.BookId, cancellationToken);
        return _mapper.Map<IEnumerable<BookBorrowingDTO>>(borrowings);
    }
}