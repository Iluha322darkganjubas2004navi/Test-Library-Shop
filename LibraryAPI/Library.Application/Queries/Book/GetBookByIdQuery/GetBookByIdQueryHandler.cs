using AutoMapper;
using FluentValidation;
using Library.Application.Exceptions;
using Library.Application.Queries.Book.GetBookByIdQuery;
using Library.Domain.DTOs;
using Library.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Queries.Book.GetBookByIdQuery;

public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookDTO>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<GetBookByIdQuery> _validator;

    public GetBookByIdQueryHandler(IBookRepository bookRepository, IMapper mapper, IValidator<GetBookByIdQuery> validator)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<BookDTO> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var existingBook = await _bookRepository.GetBookByIdAsync(request.BookId, cancellationToken);

        if (existingBook == null)
        {
            throw new NotFoundException($"Book with id '{request.BookId}' not found.");
        }

        return _mapper.Map<BookDTO>(existingBook);
    }
}
