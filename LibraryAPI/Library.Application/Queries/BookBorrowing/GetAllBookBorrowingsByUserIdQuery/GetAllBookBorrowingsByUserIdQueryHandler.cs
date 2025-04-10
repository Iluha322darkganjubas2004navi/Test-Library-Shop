using AutoMapper;
using FluentValidation;
using Library.Application.Exceptions;
using Library.Application.Queries.BookBorrowing.GetAllBookBorrowingsByUserIdQuery;
using Library.Domain.DTOs;
using Library.Infrastructure.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Queries.BookBorrowing.GetAllBookBorrowingsByUserIdQuery;

public class GetAllBookBorrowingsByUserIdQueryHandler : IRequestHandler<GetAllBookBorrowingsByUserIdQuery, IEnumerable<BookBorrowingDTO>>
{
    private readonly IBookBorrowingRepository _bookBorrowingRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<GetAllBookBorrowingsByUserIdQuery> _validator;

    public GetAllBookBorrowingsByUserIdQueryHandler(IBookBorrowingRepository bookBorrowingRepository,
                                                     IUserRepository userRepository,
                                                     IMapper mapper,
                                                     IValidator<GetAllBookBorrowingsByUserIdQuery> validator)
    {
        _bookBorrowingRepository = bookBorrowingRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<IEnumerable<BookBorrowingDTO>> Handle(GetAllBookBorrowingsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var existingUser = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (existingUser == null)
        {
            throw new NotFoundException($"User with id '{request.UserId}' not found.");
        }

        var borrowings = await _bookBorrowingRepository.GetBookBorrowingsByUserIdAsync(request.UserId, cancellationToken);
        return _mapper.Map<IEnumerable<BookBorrowingDTO>>(borrowings);
    }
}