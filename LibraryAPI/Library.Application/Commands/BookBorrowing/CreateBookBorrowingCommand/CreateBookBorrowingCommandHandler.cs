using AutoMapper;
using FluentValidation;
using Library.Application.Commands.BookBorrowing.CreateBookBorrowingCommand;
using Library.Application.Exceptions;
using Library.Domain.DTOs;
using Library.Domain.Entities;
using Library.Infrastructure.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Commands.BookBorrowing.CreateBookBorrowingCommand;

public class CreateBookBorrowingCommandHandler : IRequestHandler<CreateBookBorrowingCommand, BookBorrowingDTO>
{
    private readonly IBookBorrowingRepository _bookBorrowingRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateBookBorrowingCommand> _validator;

    public CreateBookBorrowingCommandHandler(IBookBorrowingRepository bookBorrowingRepository,
                                            IBookRepository bookRepository,
                                            IUserRepository userRepository,
                                            IMapper mapper,
                                            IValidator<CreateBookBorrowingCommand> validator)
    {
        _bookBorrowingRepository = bookBorrowingRepository;
        _bookRepository = bookRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<BookBorrowingDTO> Handle(CreateBookBorrowingCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var existingBook = await _bookRepository.GetByIdAsync(request.CreateBookBorrowingDto.BookId, cancellationToken);
        if (existingBook == null)
        {
            throw new NotFoundException($"Book with id '{request.CreateBookBorrowingDto.BookId}' not found.");
        }

        var existingUser = await _userRepository.GetByIdAsync(request.CreateBookBorrowingDto.UserId, cancellationToken);
        if (existingUser == null)
        {
            throw new NotFoundException($"User with id '{request.CreateBookBorrowingDto.UserId}' not found.");
        }

        var bookBorrowing = _mapper.Map<Domain.Entities.BookBorrowing>(request.CreateBookBorrowingDto);
        existingBook.IsBorrowed = true;
        await _bookRepository.UpdateAsync(existingBook, cancellationToken);
        await _bookBorrowingRepository.AddAsync(bookBorrowing, cancellationToken);
        return _mapper.Map<BookBorrowingDTO>(bookBorrowing);
    }
}