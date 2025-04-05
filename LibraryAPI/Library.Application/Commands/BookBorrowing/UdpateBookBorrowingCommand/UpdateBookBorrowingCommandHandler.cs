using AutoMapper;
using FluentValidation;
using Library.Application.Commands.BookBorrowing.UpdateBookBorrowingCommand;
using Library.Application.Exceptions;
using Library.Domain.DTOs;
using Library.Domain.Entities;
using Library.Infrastructure.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Commands.BookBorrowing.UpdateBookBorrowingCommand;

public class UpdateBookBorrowingCommandHandler : IRequestHandler<UpdateBookBorrowingCommand, BookBorrowingDTO>
{
    private readonly IBookBorrowingRepository _bookBorrowingRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<UpdateBookBorrowingCommand> _validator;

    public UpdateBookBorrowingCommandHandler(IBookBorrowingRepository bookBorrowingRepository,
                                            IBookRepository bookRepository,
                                            IUserRepository userRepository,
                                            IMapper mapper,
                                            IValidator<UpdateBookBorrowingCommand> validator)
    {
        _bookBorrowingRepository = bookBorrowingRepository;
        _bookRepository = bookRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<BookBorrowingDTO> Handle(UpdateBookBorrowingCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var existingBorrowing = await _bookBorrowingRepository.GetByIdAsync(request.UpdateBookBorrowingDto.Id);
        if (existingBorrowing == null)
        {
            throw new NotFoundException($"BookBorrowing with id '{request.UpdateBookBorrowingDto.Id}' not found.");
        }

        var existingBook = await _bookRepository.GetByIdAsync(request.UpdateBookBorrowingDto.BookId);
        if (existingBook == null)
        {
            throw new NotFoundException($"Book with id '{request.UpdateBookBorrowingDto.BookId}' not found.");
        }

        var existingUser = await _userRepository.GetByIdAsync(request.UpdateBookBorrowingDto.UserId);
        if (existingUser == null)
        {
            throw new NotFoundException($"User with id '{request.UpdateBookBorrowingDto.UserId}' not found.");
        }

        _mapper.Map(request.UpdateBookBorrowingDto, existingBorrowing);
        await _bookBorrowingRepository.UpdateAsync(existingBorrowing);
        return _mapper.Map<BookBorrowingDTO>(existingBorrowing);
    }
}